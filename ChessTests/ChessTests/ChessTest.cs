using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

namespace ChessTests
{
    /// <summary>
    /// Тесты для решения Chess
    /// </summary>
    [TestClass]
    public class ChessTest
    {
        /// <summary>
        /// Тест проверяет работу метода SwitchPlayer на корректных данных
        /// </summary>
        [TestMethod]
        public void Test_Chess_Form1_SwitchPlayer()
        {
            var form1 = new Chess.Form1();
            var form2 = new Chess.Form1();

            form1.currPlayer = 1;
            form1.SwitchPlayer();

            Assert.AreEqual(2, form1.currPlayer);
        }
        /// <summary>
        /// Тест проверяет работу метода SwitchPlayer на некорректных данных
        /// </summary>
        [TestMethod]
        public void Test_Chess_Form1_SwitchPlayerWithError()
        {
            var form1 = new Chess.Form1();
            var form2 = new Chess.Form1();

            form1.currPlayer = 12;
            form1.SwitchPlayer();

            Assert.AreEqual(1, form1.currPlayer);
        }
        /// <summary>
        /// Тест проверяет работу метода ActivateAllButtons
        /// </summary>
        [TestMethod]
        public void Test_Chess_Form1_ActivateAllButtons()
        {
            var form1 = new Chess.Form1();
            var form2 = new Chess.Form1();

            form1.ActivateAllButtons();

            var resActButtons = false;
            for (int i = 0; i < 8; i++)
            {
                if (resActButtons)
                {
                    break;
                }
                for (int j = 0; j < 8; j++)
                {
                    if (form1.butts[i, j].Enabled != true)
                    {
                        resActButtons = true; 
                        break;
                    }
                }
            }
            Assert.IsFalse(resActButtons);
        }
        /// <summary>
        /// Тест проверяет работу метода DeactivateAllButtons
        /// </summary>
        [TestMethod]
        public void Test_Chess_Form1_DeactivateAllButtons()
        {
            var form1 = new Chess.Form1();
            var form2 = new Chess.Form1();

            form1.ActivateAllButtons();
            form1.DeactivateAllButtons();

            var resActButtons = false;
            for (int i = 0; i < 8; i++)
            {
                if (resActButtons)
                {
                    break;
                }
                for (int j = 0; j < 8; j++)
                {
                    if (form1.butts[i, j].Enabled == true)
                    {
                        resActButtons = true;
                        break;
                    }
                }
            }
            Assert.IsFalse(resActButtons);
        }
    }
}
