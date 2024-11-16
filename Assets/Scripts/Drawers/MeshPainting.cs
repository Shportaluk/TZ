using UnityEngine;

[RequireComponent(typeof(Collider))]
public class MeshPainting : MonoBehaviour
{
	[SerializeField] private Renderer _renderer;
	[SerializeField] private Material _paintMaterial;
	
	private RenderTexture _paintRenderTexture;
	private RenderTexture _tempRenderTexture;

	private Material _mat;
	private Color _defaultColor;


    private void Awake()
	{
		_mat = new Material(_renderer.material);
		_renderer.material = _mat;
	}

	public void Paint(Vector2 uvPosition)
	{
		Check();

		if (_tempRenderTexture == null)
		{
			_tempRenderTexture = new RenderTexture(_paintRenderTexture.width, _paintRenderTexture.height, 0);
		}

		_paintMaterial.SetVector("_PaintPosition", uvPosition);
		Graphics.Blit(_paintRenderTexture, _tempRenderTexture);
		Graphics.Blit(_tempRenderTexture, _paintRenderTexture, _paintMaterial);
	}

	private void Check()
	{
		if (_paintRenderTexture != null && _mat.mainTexture == _paintRenderTexture)
			return;

		if (_mat.mainTexture != null)
		{
			if (_paintRenderTexture == null)
				_paintRenderTexture = new RenderTexture(_mat.mainTexture.width, _mat.mainTexture.height, 0);

			Graphics.Blit(_mat.mainTexture, _paintRenderTexture);
			_mat.mainTexture = _paintRenderTexture;
		}
		else
		{
			if (_paintRenderTexture == null)
				_paintRenderTexture = new RenderTexture(1024, 1024, 0);

			Texture2D ClearTexture = new Texture2D(1, 1);
			ClearTexture.SetPixel(0, 0, _defaultColor);
			Graphics.Blit(ClearTexture, _paintRenderTexture);
			_mat.mainTexture = _paintRenderTexture;

		}
	}
}