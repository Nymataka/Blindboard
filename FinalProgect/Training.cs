using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Blindboard
{
    class Training
    {
        int x = 1080, y = 30, width = 160, height = 70;
        int colorTime = 0, levelTime;
        string levelName;
        public Timer clickTimer; // Таймер мигания клавиши, таймер прохождения
        public AddElement addElement;

        Dictionary<Keys, string> keys = new Dictionary<Keys, string>(74); // Словарь из 74 значений, ключ - клавиша в системе, значение - клавиша на экрана

        List<Keys> nameKeys = new List<Keys>() {Keys.Escape, Keys.F1, Keys.F2, Keys.F3, Keys.F4, Keys.F5, // клавиши в системе
            Keys.F6, Keys.F7, Keys.F8, Keys.F9, Keys.F11, Keys.F12, Keys.Scroll, Keys.Pause, Keys.Oemtilde, Keys.D1, Keys.D2, Keys.D3, Keys.D4,
            Keys.D5, Keys.D6, Keys.D7, Keys.D8, Keys.D9, Keys.D0, Keys.OemMinus, Keys.Oemplus, Keys.Back, Keys.Insert, Keys.Home, Keys.PageUp,
            Keys.Tab, Keys.Q, Keys.W, Keys.E, Keys.R, Keys.T, Keys.Y, Keys.U, Keys.I, Keys.O, Keys.P, Keys.OemOpenBrackets, Keys.OemCloseBrackets,
            Keys.Oem5, Keys.Delete, Keys.End, Keys.PageDown, Keys.CapsLock, Keys.A, Keys.S, Keys.D, Keys.F, Keys.G, Keys.H, Keys.J, Keys.K, Keys.L,
            Keys.Oem1, Keys.Oem7, Keys.Z, Keys.X, Keys.C, Keys.V, Keys.B, Keys.N, Keys.M, Keys.Oemcomma, Keys.OemPeriod, Keys.OemQuestion, Keys.Up,
            Keys.Left, Keys.Down, Keys.Right};

        List<string> symbols; // Список символов, содержание которого зависит от уровня
        List<int> listNumbers = new List<int>();
        Label[] labels = new Label[74];
        Label labelRight = new Label();
        Label labelLevel = new Label();
        Label labelLevelName = new Label();

        public string help = "Вам предстоит нажимать на клавишу своей клавиатуры аналогичную той, что будет загораться на экране синим цветом. " +
            "На нажатие вам дается 7 или 3 секунды, в засимости от уровня, после чего начнут моргать другие клавиши в случайном порядке.";
        public void ButtonLevelClick(object sender, MouseEventArgs e) // Выбор уровня
        {
            levelName = ((Button)sender).Text;
            if (((Button)sender).Text == addElement.easy)
                MakeEasyList();
            else if (((Button)sender).Text == addElement.hard)
                MakeHardList();
            Start();
        }
        private void MakeEasyList() // Символы легкого уровня с русккой раскладной и без цифр
        {
            symbols = new List<string>() {"Esc", "f1", "f2", "f3", "f4", "f5", "f6", "f7", "f8", "f9", "f11", "f12", "Scroll\nLock", "Pause\nBreak", 
            "~ Ё\n`", "!\n1", "@ \n2", "# №\n3", "$ ;\n4", "%\n5", "^ :\n6", "?\n7", "*\n8", "(\n9", ")\n0", "-\n_", "+\n=", "⌫", "Insert", "Home", "Page\nUp", 
            "Tab", "Q\nЙ", "W\nЦ", "E\nУ", "R\nК", "T\nЕ", "Y\nН", "U\nГ", "I\nШ", "O\nЩ", "P\nЗ", "{\n[ Х", "}\n] Ъ", "\\ | /", "Del", "End", "Page\nDown", 
            "Caps\nLock", "A\nФ", "S\nЫ", "D\nВ", "F\nА", "G\nП", "H\nР", "J\nО", "K\nЛ", "L\nД", ":\n; Ж", "'\nЭ", 
            "Z\nЯ", "X\nЧ", "C\nС", "V\nМ", "B\nИ", "N\nТ", "M\nЬ", "<\n, Б",">\n. Ю", "? ,\n/ .", "↑", "←", "↓", "→"};
            levelTime = 7;
        }
        private void MakeHardList() // Символы сложного уровня без русской расскладки и цифр
        {
            symbols = new List<string>() {"Esc", "f1", "f2", "f3", "f4", "f5", "f6", "f7", "f8", "f9", "f11", "f12", "Scroll\nLock", "PB", 
            "~\n`", "!", "@", "#", "$", "%", "^", "?", "*", "(", ")", "-\n_", "+\n=", "⌫", "INS", "HM", "PU",
            "Tab", "Q", "W", "E", "R", "T", "Y", "U", "I", "O", "P", "{\n[", "}\n]", "| \\", "DEL", "END", "PD", 
            "Caps\nLock", "A", "S", "D", "F", "G", "H", "J", "K", "L", ":\n;", "'", 
            "Z", "X", "C", "V", "B", "N", "M", "<\n,",">\n.", "?\n/", "↑", "←", "↓", "→"};
            levelTime = 3;
        }
        private void Start() // Добавление всех необходимых лейблов и кнопок, запуск главного метода
        {
            addElement.CreateButton(addElement.buttonRefresh, "Начать заново", 800, 600, 440, height, addElement.ButtonRefreshClick, addElement.panelMain);
            addElement.CreateButton(addElement.buttonExit, "Выйти", x, 505, width, height, addElement.ButtonExitClick, addElement.panelMain);

            addElement.CreateLabel(addElement.labelMistake, "Ошибок: 0", x, 30, width, height, addElement.panelMain);
            addElement.CreateLabel(addElement.labelTime, "Время: 00:00", x, 220, width, height, addElement.panelMain);
            addElement.CreateLabel(labelRight, "Правильных нажатий: 0", x, 125, width, height, addElement.panelMain);
            addElement.CreateLabel(labelLevel, "Уровень:", x, 315, width, height, addElement.panelMain);
            addElement.CreateLabel(labelLevelName, levelName, x, 410, width, height, addElement.panelMain);

            addElement.labelMistake.ForeColor = Color.Tomato;
            labelRight.ForeColor = Color.LimeGreen;
            Main();
        }
        private void Main() // Добавление клавиш на панель, запуск таймера
        {
            x = 40;
            for (int i = 0; i < symbols.Count; i++)
                keys.Add(nameKeys[i], symbols[i]);
            for (int i = 0; i < labels.Length; i++)
            {
                listNumbers.Add(i);
                addElement.randomSymbols = addElement.random.Next(symbols.Count);
                labels[i] = new Label();
                addElement.CreateLabel(labels[i], symbols[addElement.randomSymbols], x, y, 70, 70, addElement.panelMain, 12);
                symbols.RemoveAt(addElement.randomSymbols);
                x += 95;
                if (x > 1050)
                {
                    y += 95;
                    x = 40;
                }
            }
            addElement.form.KeyDown += new KeyEventHandler(FormKeyDown);
            clickTimer.Tick += new EventHandler(ClickTimerTick);

            addElement.form.KeyPreview = true;
            clickTimer.Enabled = true;

            addElement.randomSymbols = addElement.random.Next(listNumbers.Count);
            labels[listNumbers[addElement.randomSymbols]].BackColor = Color.Cyan;
            addElement.allTimeTimer.Enabled = true;
            addElement.panelLevel.Visible = false;
            addElement.panelMain.Visible = true;
        }
        private void FormKeyDown(object sender, KeyEventArgs e) // Событие, нажата какая-то клавиша
        {
            clickTimer.Enabled = false;
            if (listNumbers.Count != 0)
                foreach (var item in keys)
                    if (e.KeyData == item.Key)
                    {
                        if (item.Value == labels[listNumbers[addElement.randomSymbols]].Text)
                        {
                            addElement.right++;
                            labelRight.Text = $"Правильных нажатий: {addElement.right}";
                            Result(Color.Lime);
                        }
                        else
                        {
                            addElement.AddMistake();
                            Result(Color.Tomato);
                        }
                        break;
                    }
        }
        private void ClickTimerTick(object sender, EventArgs e) // Таймер мигания текущей клавиши
        {
            colorTime++;
            if (colorTime % 2 != 0)
                labels[listNumbers[addElement.randomSymbols]].BackColor = SystemColors.Control;
            else
                labels[listNumbers[addElement.randomSymbols]].BackColor = Color.Cyan;
            if (colorTime == levelTime)
            {
                addElement.AddMistake();
                Result(Color.Tomato);
            }
        }
        private void Result(Color newColor) // Получить результат нажатия и перекрасить в соответствующий текст
        {
            labels[listNumbers[addElement.randomSymbols]].BackColor = newColor;
            listNumbers.RemoveAt(addElement.randomSymbols);
            if (listNumbers.Count == 0)
            {
                clickTimer.Enabled = false;
                addElement.form.KeyPreview = false;
                addElement.GetGreat();
            }
            else
            {
                addElement.randomSymbols = addElement.random.Next(listNumbers.Count);
                clickTimer.Enabled = true;
                labels[listNumbers[addElement.randomSymbols]].BackColor = Color.Cyan;
                colorTime = 0;
            }
        }
    }
}
