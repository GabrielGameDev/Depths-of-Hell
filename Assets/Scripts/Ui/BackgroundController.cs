using UnityEngine;
using UnityEngine.UI;

public class BackgroundController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Image _image;

    [Header("Width")]
    [SerializeField] private string _widthParameter;
    [SerializeField, Range(0, 2)] private float _widthValueToSet;
    [Header("Height")]
    [SerializeField] private string _heightParameter;
    [SerializeField, Range(0, 2)] private float _heightValueToSet;
    [Header("Bottom Color")]
    [SerializeField] private string _bottomColorParameter;
    [SerializeField] private Color _bottomColorToSet;
    [Header("Top Color")]
    [SerializeField] private string _topColorParameter;
    [SerializeField] private Color _topColorToSet;
    [Header("Halftone Mask")]
    [SerializeField] private string _halftoneControllerParameter;
    [SerializeField, Range(-5, 5)] private float _halftoneControllerValueToSet;
    [Header("Halftone Size")]
    [SerializeField] private string _halftoneSizeParameter;
    [SerializeField] private float _halftoneSizeValueToSet;
    [Header("Halftone Sides")]
    [SerializeField] private string _halftoneSidesParameter;
    [SerializeField] private float _halftoneSidesValueToSet;
    [Header("Halftone Fade")]
    [SerializeField] private string _halftoneFadeParameter;
    [SerializeField, Range(0,3)] private float _halftoneFadeValueToSet;
    [Header("Halftone Rotate")]
    [SerializeField] private string _halftoneRotateParameter;
    [SerializeField, Range(0, 360)] private float _halftoneRotateValueToSet;



    private void Update()
    {
        SetMaterial();
    }
    private void OnValidate()
    {
        SetMaterial();
    }

    private void SetMaterial()
    {
        _image.materialForRendering.SetColor(_topColorParameter, _topColorToSet);
        _image.materialForRendering.SetColor(_bottomColorParameter, _bottomColorToSet);
        _image.materialForRendering.SetFloat(_heightParameter, _heightValueToSet);
        _image.materialForRendering.SetFloat(_widthParameter, _widthValueToSet);
        _image.materialForRendering.SetFloat(_halftoneControllerParameter, _halftoneControllerValueToSet);
        _image.materialForRendering.SetFloat(_halftoneSizeParameter, _halftoneSizeValueToSet);
        _image.materialForRendering.SetFloat(_halftoneSidesParameter, _halftoneSidesValueToSet);
        _image.materialForRendering.SetFloat(_halftoneFadeParameter, _halftoneFadeValueToSet);
        _image.materialForRendering.SetFloat(_halftoneRotateParameter, _halftoneRotateValueToSet);

    }
}
