using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Blindboard
{
    class Assembly
    {
        int topX = 15, topY = 20;
        int botX = 15, botY = 360, nextX = 0, width = 50;
        int maxMistake, height = 50;
        string newMeaning = "";
        bool click = false;
        public AddElement addElement;

        Dictionary<string, int> dictNextX = new Dictionary<string, int>() // Словарь отступов клавиш нижнеей панели
        {
            {"00", 70}, {"04", 35 }, {"08", 35 },
            {"20", 35},
            {"30", 55},
            {"40", 85}, {"411", 205},
            {"50", 15 }, {"51", 15}, {"52", 15}, {"53", 375}, {"54", 15}, {"55", 16}, {"56", 18}
        };

        Dictionary<string, int> dictNextWidth = new Dictionary<string, int>() // Словарь ширины клавиш нижней панели
        {
            {"113", 70},
            {"20", 35}, {"213", 34},
            {"30", 55}, {"312", 84},
            {"40", 87}, {"411", 124},
            {"50", 16}, {"51", 16}, {"52", 16}, {"53", 377}, {"54", 15}, {"55", 18}, {"56", 20}, {"57", 20}
        };

        List<List<string>> symbols = new List<List<string>>(); // Список символов, содержание которого зависит от уровня
        List<string> symbolsCopy = new List<string>();
        Button buttonNow = new Button();

        public string help = "Соберите клавиатуру. С помощью мышки выберите сверху клавишу, а снизу поставьте ее туда, где она должна быть. " +
            "Выбранная сейчас клавиша показывается в правом верхнем углу, а количество ошибок снизу справа. " +
            "На легком уровне обычная раскладка, лимит ошибок 100, на сложном нет цифр и русской раскладки, а лимит ошибок 20.";
        public void ButtonLevelClick(object sender, MouseEventArgs e) // Выбор уровня
        {
            if (((Button)sender).Text == addElement.easy)
                MakeEasyList();
            else if (((Button)sender).Text == addElement.hard)
                MakeHardList();
            Start();
        }
        private void MakeEasyList() // Символы легкого уровня с русккой раскладной и без цифр
        {
            symbols.Add(new List<string> {"Esc", "f1", "f2", "f3", "f4", "f5", "f6", "f7", "f8", "f9", "f10", "f11", "f12", "PrtSc\nSysRq", "Scroll\nLock", "Pause\nBreak"});
            symbols.Add(new List<string> {"~ Ё\n`", "!\n1", "@ \n2", "# №\n3", "$ ;\n4", "%\n5", "^ :\n6", "?\n7", "*\n8", "(\n9", ")\n0", "-\n_", "+\n=", "⌫", "Insert", "Home", "Page\nUp"});
            symbols.Add(new List<string> {"Tab", "Q\nЙ", "W\nЦ", "E\nУ", "R\nК", "T\nЕ", "Y\nН", "U\nГ", "I\nШ", "O\nЩ", "P\nЗ", "{\n{ Х", "}\n} Ъ", "\\ | /", "Delet", "End", "Page\nDown"});
            symbols.Add(new List<string> {"Caps\nLock", "A\nФ", "S\nЫ", "D\nВ", "F\nА", "G\nП", "H\nР", "J\nО", "K\nЛ", "L\nД", ":\n; Ж", "'\nЭ", "Enter"});
            symbols.Add(new List<string> {"Shift", "Z\nЯ", "X\nЧ", "C\nС", "V\nМ", "B\nИ", "N\nТ", "M\nЬ", "<\n, Б", ">\n. Ю", "? ,\n/ .", "Shift", "↑"});
            symbols.Add(new List<string> { "Ctrl", "Win", "Alt", "Space", "Alt", "Fn", "≣ Menu", "Ctrl", "←", "↓", "→"});
            maxMistake = 100;
        }
        private void MakeHardList() // Символы сложного уровня без русской расскладки и цифр
        {
            symbols.Add(new List<string> {"Esc", "f1", "f2", "f3", "f4", "f5", "f6", "f7", "f8", "f9", "f10", "f11", "f12", "PS", "SL", "PB"});
            symbols.Add(new List<string> {"~`", "!", "@", "#", "$", "%", "^", "?", "*", "(", ")", "-\n_", "+\n=", "⌫", "INS", "HM", "PU"});
            symbols.Add(new List<string> {"↹", "Q", "W", "E", "R", "T", "Y", "U", "I", "O", "P", "{\n{", "}\n}", "| \\", "DEL", "END", "PD"});
            symbols.Add(new List<string> {"Caps\nLock", "A", "S", "D", "F", "G", "H", "J", "K", "L", ":\n;", "'", "Enter"});
            symbols.Add(new List<string> {"⇧", "Z", "X", "C", "V", "B", "N", "M", "<\n,", ">\n.", "?\n/", "⇧", "↑"});
            symbols.Add(new List<string> {"Ctrl", "Win", "Alt", "Space", "Alt", "Fn", "≣ Menu", "Ctrl", "←", "↓", "→"});
            maxMistake = 20;
        }
        private void Start() // Добавление всех необходимых лейблов и кнопок, запуск главного метода
        {
            addElement.CreateButton(addElement.buttonRefresh, "Начать заново", 990, 240, 275, height, addElement.ButtonRefreshClick, addElement.panelMain);
            addElement.CreateButton(addElement.buttonExit, "Выйти", 990, 295, 275, height, addElement.ButtonExitClick, addElement.panelMain);
            addElement.CreateButton(buttonNow, "", 1215, 20, 50, height, null, addElement.panelMain, false);

            addElement.CreateLabel(addElement.labelMistake, "Ошибок: 0", 1075, 525, 190, height, addElement.panelMain, 16);
            addElement.CreateLabel(addElement.labelTime, "Время: 00:00", 990, 185, 275, height, addElement.panelMain, 16);

            addElement.labelMistake.ForeColor = Color.Tomato;
            buttonNow.ForeColor = Color.MediumBlue;
            buttonNow.Visible = false;
            Main();
        }
        private void Main() // Добавление клавиш на панель, запуск таймера
        {
            foreach (var list in symbols)
                foreach (var item in list)
                    symbolsCopy.Add(item);
            for (int i = 0; i < symbols.Count; i++)
            {
                for (int j = 0; j < symbols[i].Count; j++)
                {
                    Button topPanelButton = new Button();
                    Button botPanelButton = new Button();

                    // Верхняя панель
                    addElement.randomSymbols = addElement.random.Next(symbolsCopy.Count);
                    addElement.CreateButton(topPanelButton, symbolsCopy[addElement.randomSymbols], topX, topY, width, height, TopButtonClick, addElement.panelMain, false);
                    symbolsCopy.Remove(symbolsCopy[addElement.randomSymbols]);
                    topX += 75;

                    // Нижняя панель
                    if (i != 3 & i != 4 & j == (symbols[i].Count - 3))
                        botX = 1075;
                    if (dictNextX.ContainsKey($"{i}{j}"))
                        nextX = dictNextX[$"{i}{j}"];
                    if (dictNextWidth.ContainsKey($"{i}{j}"))
                        width += dictNextWidth[$"{i}{j}"];
                    addElement.CreateButton(botPanelButton, "", botX, botY, width, height, BotButtonClick, addElement.panelMain, false);
                    botPanelButton.Name = $"{symbols[i][j]}";
                    botX += nextX + 70;
                    nextX = 0;
                    width = 50;
                }
                topX = botX = 15;
                topY += 55;
                botY += 55;
            }
            addElement.allTimeTimer.Enabled = true;
            addElement.panelLevel.Visible = false;
            addElement.panelMain.Visible = true;
        }
        private void TopButtonClick(object sender, MouseEventArgs e) // Нажатие кнопки верхней панели
        {
            if (click)
                return;
            click = true;
            newMeaning = ((Button)sender).Text;
            addElement.panelMain.Controls.Remove(((Button)sender));
            buttonNow.Visible = true;
            buttonNow.Text = newMeaning;
        }
        private void BotButtonClick(object sender, MouseEventArgs e) // Нажатие кнопки нижней нанели
        {
            if (newMeaning != "" & ((Button)sender).Text == "")
            {
                if (newMeaning == ((Button)sender).Name)
                {
                    click = false;
                    addElement.right++;
                    newMeaning = "";
                    buttonNow.Visible = false;
                    ((Button)sender).Text = ((Button)sender).Name;
                    if (addElement.right == 87)
                        addElement.GetGreat();
                }
                else
                {
                    addElement.AddMistake();
                    if (addElement.mistake == maxMistake)
                    {
                        addElement.allTimeTimer.Enabled = false;
                        MessageBox.Show($"Вы допустили {addElement.mistake} ошибок, попробуйте ещё раз");
                        Application.Restart();
                    }
                }
            }
        }
    }
}
