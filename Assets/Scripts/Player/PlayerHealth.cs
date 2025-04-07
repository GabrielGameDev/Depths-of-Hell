using System;
using System.Collections;
using UnityEngine;
using System.Linq;
using Unity.Cinemachine;

public class PlayerHealth : MonoBehaviour
{
	public GameObject deathFx;
	[SerializeField] private float freezeDuration = 0.1f; // Tempo total do freeze
	[SerializeField] private float returnToNormalTime = 0.5f; // Tempo para voltar ao normal
	[SerializeField] private float minTimeScale = 0.1f;
	bool isFreezing = false;
	PlayerController playerController;
	CinemachineImpulseSource impulseSource;
	public GameObject playerMesh;
	bool isDead;

	private void Awake()
	{
		playerController = GetComponent<PlayerController>();
		impulseSource = GetComponent<CinemachineImpulseSource>();
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(LevelManager.instance.isGameOver) return; // Evita múltiplas ativações
		if(isDead) return; // Evita múltiplas ativações
		Damager damager = collision.GetComponent<Damager>();
		if (damager != null)
		{
			impulseSource.GenerateImpulse();
			GameObject newDeathFx = Instantiate(deathFx, transform.position, Quaternion.identity);
			newDeathFx.SetActive(true);	
			if(damager.isLava)
				LevelManager.instance.GameOver(true);
			else
			LevelManager.instance.GameOver(LevelManager.hardcoreMode);

			if(damager.isLava || LevelManager.hardcoreMode)
			{
				isDead = true;
			}
			

			StartCoroutine(FreezeFrames());
			
		}
		
	}

	//void TeleportToNearestPlatform(Vector3 damagerPosition)
	//{
	//	Collider2D[] platforms = Physics2D.OverlapCircleAll(transform.position, 20f, playerController.groundLayer);
	//	if (platforms.Length == 0)
	//	{
	//		Debug.LogWarning("Nenhuma plataforma encontrada no raio!");
	//		return;
	//	}

	//	// Ordena as plataformas por distância
	//	var nearestPlatform = platforms
	//		.Where(p => p.transform.position.y > (damagerPosition.y + 3))
	//		.OrderBy(p => Vector3.Distance(transform.position, p.transform.position))
	//		.ToArray();

	//	Collider2D nearestPlatformAbove = nearestPlatform.First();

	//	// Calcula posição de teleporte (centro da plataforma + offset)
	//	Vector3 targetPosition = new Vector3(
	//		nearestPlatformAbove.transform.position.x,
	//		nearestPlatformAbove.transform.position.y + 2,
	//		nearestPlatformAbove.transform.position.z
	//	);

	//	// Executa o teleporte
	//	transform.position = targetPosition;
	//	GameObject newDeathFx = Instantiate(deathFx, transform.position, Quaternion.identity);
	//	newDeathFx.SetActive(true);
	//	Debug.Log($"Teleportado para plataforma acima! Altura: {targetPosition.y}");
	//}

	private IEnumerator FreezeFrames()
	{
		if (isFreezing) yield break;
		isFreezing = true;
		playerController.DisableMovement(); // Desabilita o movimento do jogador
		playerController.enabled = false; // Desabilita o movimento do jogador
		if(LevelManager.hardcoreMode)
			playerMesh.SetActive(false); // Desabilita o jogador para evitar movimento
		// Congela imediatamente
		Time.timeScale = minTimeScale;
		Time.fixedDeltaTime = 0.02f * Time.timeScale; // Ajusta a física

		// Mantém congelado pelo tempo definido
		yield return new WaitForSecondsRealtime(freezeDuration);

		// Retorna gradualmente ao normal
		float elapsedTime = 0f;
		while (elapsedTime < returnToNormalTime)
		{
			Time.timeScale = Mathf.Lerp(minTimeScale, 1f, elapsedTime / returnToNormalTime);
			Time.fixedDeltaTime = 0.02f * Time.timeScale;
			elapsedTime += Time.unscaledDeltaTime;
			yield return null;
		}
		if(!LevelManager.hardcoreMode)			
			playerController.enabled = true; // Reabilita o movimento do jogador
		// Garante que voltou ao normal
		Time.timeScale = 1f;
		Time.fixedDeltaTime = 0.02f;
		isFreezing = false;
	}
}
