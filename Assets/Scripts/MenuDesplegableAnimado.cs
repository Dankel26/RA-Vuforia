using System.Collections;
using UnityEngine;

public class MenuDesplegableAnimado : MonoBehaviour
{
    [Header("Referencias de la UI")]
    public RectTransform contenedorLista;
    public CanvasGroup canvasGroupLista;

    [Header("Configuración de Animación")]
    public float duracion = 0.2f; // Tiempo en segundos

    private bool estaAbierto = false;
    private Coroutine rutinaAnimacion;

    void Start()
    {
        // Estado inicial: cerrado e invisible
        if (contenedorLista != null && canvasGroupLista != null)
        {
            contenedorLista.localScale = new Vector3(1, 0, 1); // Escala Y en 0
            canvasGroupLista.alpha = 0f;
            canvasGroupLista.interactable = false;
            canvasGroupLista.blocksRaycasts = false;
        }
    }

    public void AlternarMenu()
    {
        estaAbierto = !estaAbierto;

        if (rutinaAnimacion != null)
            StopCoroutine(rutinaAnimacion);

        rutinaAnimacion = StartCoroutine(AnimarMenu(estaAbierto));
    }

    private IEnumerator AnimarMenu(bool abrir)
    {
        float tiempo = 0f;
        Vector3 escalaInicial = contenedorLista.localScale;
        Vector3 escalaFinal = abrir ? Vector3.one : new Vector3(1, 0, 1);

        float alphaInicial = canvasGroupLista.alpha;
        float alphaFinal = abrir ? 1f : 0f;

        while (tiempo < duracion)
        {
            tiempo += Time.deltaTime;
            float progreso = tiempo / duracion;

            // Transición suave (SmoothStep)
            float curva = Mathf.SmoothStep(0f, 1f, progreso);

            contenedorLista.localScale = Vector3.Lerp(escalaInicial, escalaFinal, curva);
            canvasGroupLista.alpha = Mathf.Lerp(alphaInicial, alphaFinal, curva);

            yield return null;
        }

        // Aseguramos los valores finales e interactividad
        contenedorLista.localScale = escalaFinal;
        canvasGroupLista.alpha = alphaFinal;
        canvasGroupLista.interactable = abrir;
        canvasGroupLista.blocksRaycasts = abrir;
    }
}