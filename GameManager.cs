using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Button[] celdas;  
    public TMP_Text TurnoActivo;   // Texto para mostrar el turno actual
    public Button BotonReiniciar;

    private string JugadorEnTurno = "X";  // Jugador que inicia
    private bool gameActive = true;

    void Start()
    {
        BotonReiniciar.onClick.AddListener(ReiniciarJuego);
        for (int i = 0; i < celdas.Length; i++)
        {
            int index = i;
            celdas[i].onClick.AddListener(() => CellClicked(index));
        }
        UpdateTextoTurno(); // Arreglado, faltaba el punto y coma
                            // Agregando este comentario para comprobar funcionalidad del github action

    }

    void CellClicked(int index)
    {
        if (!gameActive || celdas[index].GetComponentInChildren<TMP_Text>().text != "") return;

        celdas[index].GetComponentInChildren<TMP_Text>().text = JugadorEnTurno;
        CheckWinner();
        JugadorEnTurno = (JugadorEnTurno == "X") ? "O" : "X";
        UpdateTextoTurno();
    }

    void CheckWinner()
    {
        int[,] winPatterns = new int[,]
        {
            {0, 1, 2}, {3, 4, 5}, {6, 7, 8}, // Horizontales
            {0, 3, 6}, {1, 4, 7}, {2, 5, 8}, // Verticales
            {0, 4, 8}, {2, 4, 6}             // Diagonales
        };

        for (int i = 0; i < winPatterns.GetLength(0); i++)
        {
            if (celdas[winPatterns[i, 0]].GetComponentInChildren<TMP_Text>().text == JugadorEnTurno &&
                celdas[winPatterns[i, 1]].GetComponentInChildren<TMP_Text>().text == JugadorEnTurno &&
                celdas[winPatterns[i, 2]].GetComponentInChildren<TMP_Text>().text == JugadorEnTurno)
            {
                EndGame($"{JugadorEnTurno} ha ganado!");
                return;
            }
        }

        if (Tablerolleno())
        {
            EndGame("¡Empate!");
        }
    }

    bool Tablerolleno()
    {
        foreach (var cell in celdas)
        {
            if (cell.GetComponentInChildren<TMP_Text>().text == "") return false;
        }
        return true;
    }

    void EndGame(string message)
    {
        TurnoActivo.text = message;
        gameActive = false;
        Debug.Log(message);
    }

    void ReiniciarJuego()
    {
        foreach (var cell in celdas)
        {
            cell.GetComponentInChildren<TMP_Text>().text = "";
        }
        JugadorEnTurno = "X";
        gameActive = true;
        UpdateTextoTurno();
    }

    void UpdateTextoTurno()
    {
        TurnoActivo.text = $"Turno: {JugadorEnTurno}";
    }
}

