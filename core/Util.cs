using System;
using System.Windows.Forms;

namespace core
{
    public class Util
    {
        public static string KeyToString(KeyEventArgs e)
        {
            string result = String.Empty;
            switch (e.KeyCode)
            {
                case Keys.D0:
                case Keys.NumPad0:
                    result = "0";
                    break;
                case Keys.D1:
                case Keys.NumPad1:
                    result = "1";
                    break;
                case Keys.D2:
                case Keys.NumPad2:
                    result = "2";
                    break;
                case Keys.D3:
                case Keys.NumPad3:
                    result = "3";
                    break;
                case Keys.D4:
                case Keys.NumPad4:
                    result = "4";
                    break;
                case Keys.D5:
                case Keys.NumPad5:
                    result = "5";
                    break;
                case Keys.D6:
                case Keys.NumPad6:
                    result = "6";
                    break;
                case Keys.D7:
                case Keys.NumPad7:
                    result = "7";
                    break;
                case Keys.D8:
                case Keys.NumPad8:
                    result = "8";
                    break;
                case Keys.D9:
                case Keys.NumPad9:
                    result = "9";
                    break;
                case Keys.Decimal:
                case Keys.OemPeriod:
                    result = ".";
                    break;
                case Keys.A:
                    result = "a";
                    break;
                case Keys.B:
                    result = "b";
                    break;
                case Keys.C:
                    result = "c";
                    break;
                case Keys.D:
                    result = "d";
                    break;
                case Keys.E:
                    result = "e";
                    break;
                case Keys.F:
                    result = "f";
                    break;
                case Keys.G:
                    result = "g";
                    break;
                case Keys.H:
                    result = "h";
                    break;
                case Keys.I:
                    result = "i";
                    break;
                case Keys.J:
                    result = "j";
                    break;
                case Keys.K:
                    result = "k";
                    break;
                case Keys.L:
                    result = "l";
                    break;
                case Keys.M:
                    result = "m";
                    break;
                case Keys.N:
                    result = "n";
                    break;
                case Keys.O:
                    result = "o";
                    break;
                case Keys.P:
                    result = "p";
                    break;
                case Keys.Q:
                    result = "q";
                    break;
                case Keys.R:
                    result = "r";
                    break;
                case Keys.S:
                    result = "s";
                    break;
                case Keys.T:
                    result = "t";
                    break;
                case Keys.U:
                    result = "u";
                    break;
                case Keys.V:
                    result = "v";
                    break;
                case Keys.W:
                    result = "w";
                    break;
                case Keys.X:
                    result = "x";
                    break;
                case Keys.Y:
                    result = "y";
                    break;
                case Keys.Z:
                    result = "z";
                    break;
                case Keys.Space:
                    result = " ";
                    break;
            }
            return result;
        }
    }
}
