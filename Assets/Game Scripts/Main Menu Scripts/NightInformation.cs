using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightInformation : MonoBehaviour
{
    // Start is called before the first frame update
    public static int[] AiLevels = new int[6];
    public static int GameNight;
    /// <summary>
    /// Night 1, Night 2, Night 3, Night 4, Night 5, Night 6
    /// Dune, Criket, Lioness, Reggie, Mikey, Sally
    /// </summary>
    public static int[,] nightLevels = new int[6, 6];
}
