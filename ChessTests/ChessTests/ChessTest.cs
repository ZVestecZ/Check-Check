﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Drawing;

namespace ChessTests
{
    /// <summary>
    /// Тесты для решения Chess
    /// </summary>
    [TestClass]
    public class ChessTest
    {
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
        /// <summary>
        /// Тест проверяет работу метода InsideBorder
        /// </summary>
        [TestMethod]
        public void Test_Chess_Form1_InsideBorder()
        {
            var form1 = new Chess.Form1();

            var inside = form1.InsideBorder(6, 7);

            Assert.IsTrue(inside);
        }
        /// <summary>
        /// Тест проверяет работу метода InsideBorder для точки за пределами поля
        /// </summary>
        [TestMethod]
        public void Test_Chess_Form1_InsideBorderWithError()
        {
            var form1 = new Chess.Form1();

            var inside = form1.InsideBorder(7, -1);

            Assert.IsFalse(inside);
        }
        /// <summary>
        /// Тест проверяет работу метода DeterminePath 
        /// </summary>
        [TestMethod]
        public void Test_Chess_Form1_DeterminePath()
        {
            var form1 = new Chess.Form1();

            form1.map[5, 3] = 21;
            form1.currPlayer = 1;

            form1.DeterminePath(5, 3);
            var res = form1.butts[5, 3].BackColor == Color.Yellow;

            Assert.IsTrue(res);
        }
        /// <summary>
        /// Тест проверяет работу метода DeterminePath в случае невозможного хода
        /// </summary>
        [TestMethod]
        public void Test_Chess_Form1_DeterminePathWithError()
        {
            var form1 = new Chess.Form1();

            form1.map[5, 3] = 11;
            form1.currPlayer = 1;

            form1.DeterminePath(5, 3);
            var res = form1.butts[5, 3].BackColor == Color.Yellow;

            Assert.IsFalse(res);
        }
        /// <summary>
        /// Тест проверяет работу метода ShowDiagonal
        /// </summary>
        [TestMethod]
        public void Test_Chess_Form1_ShowDiagonal()
        {
            var form1 = new Chess.Form1();

            form1.ShowDiagonal(5, 3);

            var res = true;
            var diagI = new List<int>() { 4, 6, 4, 3, 2, 6 };
            var diagJ = new List<int>() { 2, 4, 4, 5, 6, 2 };

            for (int i = 0; i < diagI.Count; i++)
            {
                if (form1.butts[diagI[i], diagJ[i]].BackColor != Color.Yellow)
                {
                    res = false;
                    break;
                }
            }
            Assert.IsTrue(res);
        }
        /// <summary>
        /// Тест проверяет работу метода ShowVerticalHorizontal
        /// </summary>
        [TestMethod]
        public void Test_Chess_Form1_ShowVerticalHorizontal()
        {
            var form1 = new Chess.Form1();
            form1.currPlayer = 1;
            form1.map[5, 3] = 15;
            form1.ShowVerticalHorizontal(5, 3);

            var res = true;

            for (int i = 2; i < 4; i++)
            {
                if (form1.butts[i, 3].BackColor != Color.Yellow)
                {
                    res = false;
                    break;
                }
            }
            for (int i = 0; i < 8; i++)
            {
                if (i == 3)
                {
                    continue;
                }
                if (form1.butts[5, i].BackColor != Color.Yellow)
                {
                    res = false;
                    break;
                }
            }
            Assert.IsTrue(res);
        }
        /// <summary>
        /// Тест проверяет работу метода ShowVerticalHorizontal в случае отсутствия ходов
        /// </summary>
        [TestMethod]
        public void Test_Chess_Form1_ShowVerticalHorizontalNoWay()
        {
            var form1 = new Chess.Form1();

            form1.ShowVerticalHorizontal(0, 0);

            var res = true;

            for (int i = 0; i < 8; i++)
            {
                if (!res)
                {
                    break;
                }
                for (int j = 0; j < 8; j++)
                {
                    if (form1.butts[i, j].BackColor == Color.Yellow)
                    {
                        res = false;
                        break;
                    }
                }
            }
            Assert.IsTrue(res);
        }
        /// <summary>
        /// Тест проверяет работу метода ShowDiagonal в случае отсуттствия ходов
        /// </summary>
        [TestMethod]
        public void Test_Chess_Form1_ShowDiagonalNoway()
        {
            var form1 = new Chess.Form1();

            form1.ShowDiagonal(0, 0);

            var res = true;

            for (int i = 0; i < 8; i++)
            {
                if (!res)
                {
                    break;
                }
                for (int j = 0; j < 8; j++)
                {                    
                    if (form1.butts[i, j].BackColor == Color.Yellow)
                    {
                        res = false;
                        break;
                    }
                }
            }
            Assert.IsTrue(res);
        }
        /// <summary>
        /// Тест проверяет работу метода ShowKnightSteps
        /// </summary>
        [TestMethod]
        public void Test_Chess_Form1_ShowHorseSteps()
        {
            var form1 = new Chess.Form1();

            form1.ShowKnightSteps(6, 0);

            var res = form1.butts[5, 2].BackColor == Color.Yellow
                && form1.butts[7, 2].BackColor == Color.Yellow;
            Assert.IsTrue(res);
        }
        /// <summary>
        /// Тест проверяет работу метода ShowKnightSteps в случае отсутствия ходов
        /// </summary>
        [TestMethod]
        public void Test_Chess_Form1_ShowHorseStepsNoWay()
        {
            var form1 = new Chess.Form1();
            form1.map[5, 2] = 11;
            form1.map[7, 2] = 11;

            form1.ShowKnightSteps(6, 0);

            var res = form1.butts[5, 2].BackColor != Color.Yellow
                && form1.butts[7, 2].BackColor != Color.Yellow;
            Assert.IsTrue(res);
        }
        /// <summary>
        /// Тест проверяет работу метода ShowSteps для ладьи
        /// </summary>
        [TestMethod]
        public void Test_Chess_Form1_ShowSteps()
        {
            var form1 = new Chess.Form1();

            form1.ShowSteps(4, 4, 15);


            var res = true;
            for (int i = 2; i < 6; i++)
            {
                if (i == 4)
                {
                    continue;
                }
                if (form1.butts[i, 4].BackColor != Color.Yellow)
                {
                    res = false;
                    break;
                }
            }
            for (int i = 0; i < 8; i++)
            {
                if (i == 4)
                {
                    continue;
                }
                if (form1.butts[4, i].BackColor != Color.Yellow)
                {
                    res = false;
                    break;
                }
            }
            Assert.IsTrue(res);
        }
        /// <summary>
        /// Тест проверяет работу метода ShowSteps для ладьи
        /// </summary>
        [TestMethod]
        public void Test_Chess_Form1_ShowStepsForKing()
        {
            var form1 = new Chess.Form1();

            form1.ShowSteps(4, 4, 11);


            var res = true;
            for (int i = 3; i < 6; i++)
            {
                if (!res)
                {
                    break;
                }
                for (int j = 3; j < 6; j++)
                {
                    if (i == 4 && j == 4)
                    {
                        continue;
                    }
                    if (form1.butts[i, j].BackColor != Color.Yellow)
                    {
                        res = false;
                        break;
                    }
                }
            }
            Assert.IsTrue(res);
        }
        /// <summary>
        /// Тест проверяет работу метода ShowSteps в случае отсутствия ходов
        /// </summary>
        [TestMethod]
        public void Test_Chess_Form1_ShowStepsNoWay()
        {
            var form1 = new Chess.Form1();

            form1.ShowSteps(0, 0, 15);

            var res = true;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (!res)
                    {
                        break;
                    }
                    if (form1.butts[i, j].BackColor == Color.Yellow)
                    {
                        res = false;
                        break;
                    }
                }
            }
            Assert.IsTrue(res);
        }
    }
}
