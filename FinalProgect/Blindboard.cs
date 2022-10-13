using Blindboard;
using System;
using System.Windows.Forms;

namespace FinalProgect
{

    public partial class Blindboard : Form
    {
        int x = 570, width = 160, height = 70;
        string training = "Тренажёр", assembly = "Сборка";

        AddElement addElement = new AddElement();

        Panel panelStart = new Panel();
        Panel panelHelp = new Panel();

        Button buttonTraining = new Button();
        Button buttonAssembly = new Button();
        Button buttonEasy = new Button();
        Button buttonHard = new Button();
        Button buttonHelp = new Button();
        Button buttonOk = new Button();

        Label labelHelp = new Label();
        public Blindboard()
        {
            Start();
            InitializeComponent();
        }
        private void Start() // Запуск приложения, выбор режима
        {
            addElement.form = this;

            addElement.CreatePanel(panelStart);
            addElement.CreatePanel(addElement.panelLevel);
            addElement.CreatePanel(panelHelp);
            addElement.CreatePanel(addElement.panelMain);

            addElement.CreateButton(buttonTraining, training, x, 175, width, height, ButtonModeClick, panelStart);
            addElement.CreateButton(buttonAssembly, assembly, x, 275, width, height, ButtonModeClick, panelStart);
            addElement.CreateButton(addElement.buttonExit, "Выйти", x, 375, width, height, addElement.ButtonExitClick, panelStart);
            addElement.CreateButton(buttonEasy, addElement.easy, x, 175, width, height, null, addElement.panelLevel);
            addElement.CreateButton(buttonHard, addElement.hard, x, 275, width, height, null, addElement.panelLevel);
            addElement.CreateButton(buttonHelp, "Справка", x, 375, width, height, ButtonHelpClick, addElement.panelLevel);
            addElement.CreateButton(buttonOk, "ОК", x, 375, width, height, ButtonOkClick, panelHelp);

            addElement.CreateLabel(labelHelp, "Информация", 285, 175, 730, 160, panelHelp);
            panelStart.Visible = true;
        }

        private void ButtonModeClick(object sender, MouseEventArgs e) // Добавление выбраного режима в класс add_element
        {
            addElement.allTimeTimer = allTimeTimer;
            addElement.allTimeTimer.Tick += new EventHandler(addElement.AllTimeTimerTick);

            if (((Button)sender).Text == training) // Тренажёр
            {
                Training mode = new Training {clickTimer = clickTimer, addElement = addElement};
                buttonEasy.MouseClick += mode.ButtonLevelClick;
                buttonHard.MouseClick += mode.ButtonLevelClick;
                labelHelp.Text = mode.help;
            }
            else if (((Button)sender).Text == assembly) // Сборка
            {
                Assembly mode = new Assembly { addElement = addElement};
                buttonEasy.MouseClick += mode.ButtonLevelClick;
                buttonHard.MouseClick += mode.ButtonLevelClick;
                labelHelp.Text = mode.help;
            }
            panelStart.Visible = false;
            addElement.panelLevel.Visible = true;
        }
        private void ButtonHelpClick(object sender, MouseEventArgs e) // Кпонка справчника
        {
            addElement.panelLevel.Visible = false;
            panelHelp.Visible = true;
        }
        private void ButtonOkClick(object sender, MouseEventArgs e) // Выход из спровочника
        {
            panelHelp.Visible = false;
            addElement.panelLevel.Visible = true;
        }
    }
}
