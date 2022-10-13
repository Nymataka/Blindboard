using System;
using System.Drawing;
using System.Windows.Forms;

namespace Blindboard
{
    class AddElement
    {
        public int randomSymbols, mistake = 0, right = 0, allTime = 0;
        public string easy = "Легко", hard = "Сложно", result;
        public Timer allTimeTimer;
        public Random random = new Random();
        public Form form;

        public Panel panelLevel = new Panel();
        public Panel panelMain = new Panel();

        public Button buttonRefresh = new Button();
        public Button buttonExit = new Button();

        public Label labelMistake = new Label();
        public Label labelTime = new Label();

        public void CreatePanel(Panel panel) // Создать панель
        {
            panel.BackgroundImage = global::Blindboard.Properties.Resources.gradient;
            panel.Location = new Point(0, 0);
            panel.Size = new Size(1300, 750);
            panel.Visible = false;
            form.Controls.Add(panel);
        }
        public void CreateButton(Button button, string text, int x, int y, int width, int height, MouseEventHandler methodClick, Panel panel, bool isFont = true) // Создать кнопку
        {
            button.PreviewKeyDown += new PreviewKeyDownEventHandler(ButtonPreviewKeyDown);
            button.FlatStyle = FlatStyle.Flat;
            button.Text = text;
            button.Location = new Point(x, y);
            button.Size = new Size(width, height);
            if (isFont)
                button.Font = new Font("Microsoft Sans Serif", 18, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
            button.MouseClick += methodClick;
            form.Controls.Add(button);
            button.Parent = panel;
        }
        public void CreateLabel(Label label, string text, int x, int y, int width, int height, Panel panel, int fontSize = 14) // Создать label
        {
            label.BorderStyle = BorderStyle.FixedSingle;
            label.TextAlign = ContentAlignment.MiddleCenter;
            label.Text = text;
            label.Font = new Font("Microsoft Sans Serif", fontSize, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
            label.Location = new Point(x, y);
            label.Size = new Size(width, height);
            form.Controls.Add(label);
            label.Parent = panel;
        }
        public void ButtonRefreshClick(object sender, MouseEventArgs e) // Начать заново
        {
            Application.Restart();
        }
        public void ButtonExitClick(object sender, MouseEventArgs e) // Выйти из приложения
        {
            Application.Exit();
        }
        public void ButtonPreviewKeyDown(object sender, PreviewKeyDownEventArgs e) // Не реагировать на стрелочки и tab
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down || e.KeyCode == Keys.Right || e.KeyCode == Keys.Left || e.KeyCode == Keys.Tab)
                e.IsInputKey = true;
        }
        public void GetGreat() // Выдать оценку
        {
            allTimeTimer.Enabled = false;
            if (mistake == 0)
                result = "Идеальный результат.";
            else if (mistake <= 10)
                result = "Отличный результат, еще немного и будет идеально.";
            else if (mistake <= 30)
                result = "Неплохой результат, но могло быть и лучше.";
            else if (mistake <= 60)
                result = "Плохо стараешься. Давай заново.";
            else
                result = "Эй, ты даже не стараешься. Давай заново.";
            MessageBox.Show($"Ошибок: {mistake}. " + result);
        }
        public void AddMistake()
        {
            mistake++;
            labelMistake.Text = $"Ошибок: {mistake}";
        }
        public void AllTimeTimerTick(object sender, EventArgs e) // Таймер
        {
            allTime++;
            if (allTime < 10)
                labelTime.Text = $"Время: 00:0{allTime}";
            if (allTime >= 10 && allTime < 60)
                labelTime.Text = $"Время: 00:{allTime}";
            if (allTime >= 60)
            {
                if (allTime % 60 < 10)
                    labelTime.Text = string.Format("Время: 0{0}:0{1}", allTime / 60, allTime % 60);
                else
                    labelTime.Text = string.Format("Время: 0{0}:{1}", allTime / 60, allTime % 60);
            }
        }
    }
}
