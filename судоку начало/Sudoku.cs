using System;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace Судоку
{
    public partial class Form1 : Form
    {
        Grid newgeneretion = new Grid();
        save ss = new save();
        frmInput inputform;
        Label[,] lib = new Label[9, 9];
        Random rnd = new Random();
        SoundPlayer sp = new SoundPlayer("C:\\Users\\Екатерина\\source\\repos\\КУРСОВАЯ ПРОГИ 4 СЕМ\\судоку начало\\иконки\\sound.wav");

        int hide;
        int level;
        string time;

        DateTime StartTime = DateTime.Now;
        Timer timer = new Timer();

        public Form1()
        {
            InitializeComponent();
            inputform = new frmInput();
            sp.Play();
        }

        public void Initialization_lib()
        {
            foreach (var item in tablPN.Controls)
            {
                foreach (Label lbl in (item as TableLayoutPanel).Controls)
                {
                    string x = lbl.Name[lbl.Name.Length - 2].ToString();
                    int row = Convert.ToInt32(x) - 1;
                    x = lbl.Name[lbl.Name.Length - 1].ToString();
                    int col = Convert.ToInt32(x) - 1;
                    lib[row, col] = lbl;
                }
            }
            for (int i = 0; i <= 8; i++)
            {
                for (int j = 0; j <= 8; j++)
                {
                    lib[i, j].ForeColor = Color.Black;
                }
            }
        }

        public void Board(bool fromsave = false)
        {
            Initialization_lib();
            newgeneretion = new Grid();
            newgeneretion.Shaffl(hide);
        }

        public void View(int hintsCount)
        {
            Board();
            for (int i = 0; i <= 8; i++)
            {
                for (int j = 0; j <= 8; j++)
                {
                    string n;
                    n = Convert.ToString(newgeneretion.getGridij(i, j));
                    lib[i, j].Text = n;
                }
            }
            Random random = new Random();
            for (int i = 0; i < hintsCount; i++)
            {
                var rX = random.Next(9);
                var rY = random.Next(9);
                lib[rX, rY].Text = "";
                newgeneretion.setFillTypeij(rX, rY);
                lib[rX, rY].ForeColor = Color.Blue;
            }
        }

        public void View()
        {
            Grid neww = new Grid(ss.grid, ss.gridGame, ss.fillType);
            Initialization_lib();
            tablPN.Visible = true;
            BTNPlayPause.Text = "Пауза";
            BTNPlayPause.Enabled = true;
            for (int i = 0; i <= 8; i++)
            {
                for (int j = 0; j <= 8; j++)
                {
                    lib[i, j].Text = Convert.ToString(neww.getGridij(i, j));
                }
            }
            for (int i = 0; i <= 8; i++)
            {
                for (int j = 0; j <= 8; j++)
                {
                    if (neww.getfillTypeij(i, j) == true)
                    {
                        lib[i, j].Text = "";
                        lib[i, j].ForeColor = Color.Blue;
                    }
                }
            }
            for (int i = 0; i <= 8; i++)
            {
                for (int j = 0; j <= 8; j++)
                {
                    if (neww.getGridGameij(i, j) != 0)
                    {
                        lib[i, j].Text = Convert.ToString(neww.getGridGameij(i, j));
                    }

                }
            }
            hide = ss.hide;
            StatusLBL.Text = ("Нерешенных ячеек => " + hide);
            if (ss.level == 1) { rBtnEASY.Checked = true; }
            else if (ss.level == 2) { rBtnMEDIUM.Checked = true; }
            else if (ss.level == 3) { rBtnHARD.Checked = true; }
        }

        private void BTNNewGame_Click(object sender, EventArgs e)
        {
            if (rBtnEASY.Checked == true)
            {
                level = 1;
                //hide = 3;
                 hide = rnd.Next(30, 40);
            }
            else if (rBtnMEDIUM.Checked == true)
            {
                level = 2;
                hide = rnd.Next(40, 50);
            }
            else if (rBtnHARD.Checked == true)
            {
                level = 3;
                hide = rnd.Next(50, 60);
            }
            tablPN.Visible = true;
            BTNPlayPause.Enabled = true;
            StatusLBL.Text = ("Нерешенных ячеек => " + hide);
            View(hide);
            StartTime = DateTime.Now;
            timer.Start();
            BTNPlayPause.Text = "Пауза";
            btnProverka.Enabled = true;
            timer.Tick += new EventHandler(timer_Tick);
        }

        private void BTNPlayPause_Click(object sender, EventArgs e)
        {
            if (BTNPlayPause.Text == "Пауза")
            {
                tablPN.Visible = false;
                BTNPlayPause.Text = "Продолжить";
                timer.Enabled = false;
                timer.Stop();
            }
            else
            {
                tablPN.Visible = true;
                BTNPlayPause.Text = "Пауза";
                timer.Start();
            }
        }

        //обработчик события Tick
        void timer_Tick(object sender, EventArgs e)
        {
            if (BTNPlayPause.Text == "Пауза")
            {
                TimeSpan elapsed = DateTime.Now - StartTime;
                string text = "";
                int tenths = elapsed.Milliseconds / 100;
                text += elapsed.Hours.ToString("00") + ":" + elapsed.Minutes.ToString("00") + ":" + elapsed.Seconds.ToString("00") + "." + tenths.ToString("0");
                LBLTimer.Text = text;
            }
            else if (BTNPlayPause.Text == "Продолжить")
            {
                return;
            }

        }

        private void rowcol(Label lbl, out int row, out int col)
        {
            row = (Convert.ToInt32((lbl).Name[(lbl).Name.Length - 2].ToString())) - 1;
            col = (Convert.ToInt32((lbl).Name[(lbl).Name.Length - 1].ToString())) - 1;
        }

        private void lblNumber_Click(object sender, EventArgs e)
        {
            Color backcolor = (sender as Label).BackColor;
            if ((sender as Label).Text == "")
            {
                (sender as Label).BackColor = Color.MistyRose;
                inputform.ShowDialog();
                (sender as Label).ForeColor = Color.Blue;
                int x = inputform.getNumber();
                int row;
                int col;
                if (x != 0 && x != 10 && x != 100)
                {
                    (sender as Label).Text = x.ToString();
                    rowcol((sender as Label), out row, out col);
                    hide--;
                    newgeneretion.setGridGameij(x, row, col);
                }
                else if (x == 10 && x != 100)
                {
                    (sender as Label).Text = "";
                    rowcol((sender as Label), out row, out col);
                    newgeneretion.setGridGameij(0, row, col);
                }
                else if (x == 100)
                {
                    hide--;
                    rowcol((sender as Label), out row, out col);
                    newgeneretion.getfillTypeij(row, col);
                    newgeneretion.HelpCheck(sender as Label);
                }
                (sender as Label).BackColor = backcolor;
            }
            else if ((sender as Label).ForeColor == Color.Blue || (sender as Label).ForeColor == Color.Red)
            {
                (sender as Label).BackColor = Color.MistyRose;
                inputform.ShowDialog();
                (sender as Label).ForeColor = Color.Blue;
                int x = inputform.getNumber();
                int row;
                int col;
                if (x != 0 && x != 10 && x != 100)
                {
                    (sender as Label).Text = x.ToString();
                    rowcol((sender as Label), out row, out col);
                    newgeneretion.setGridGameij(x, row, col);
                }
                else if (x == 10 && x != 100)
                {
                    (sender as Label).Text = "";
                    rowcol((sender as Label), out row, out col);
                    hide++;
                    newgeneretion.setGridGameij(0, row, col);
                }
                else if (x == 100)
                {
                    rowcol((sender as Label), out row, out col);
                    newgeneretion.getfillTypeij(row, col);
                    newgeneretion.HelpCheck(sender as Label);
                }
            }
            if (hide == 0)
            {
                btnProverka_Click(null, null);
            }
                (sender as Label).BackColor = backcolor;
            StatusLBL.Text = ("Нерешенных ячеек => " + hide);
        }

        private void btnProverka_Click(object sender, EventArgs e)
        {
            newgeneretion.ProverkaGrid(lib);
        }

        private void rBtn_Click(object sender, EventArgs e)
        {
            if (BTNPlayPause.Enabled == true)
            {
                DialogResult dr = MessageBox.Show("Вы изменили уровень. Хотите начать новую игру?", "Новая игра", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    if (rBtnEASY.Checked == true) { level = 1; }
                    else if (rBtnMEDIUM.Checked == true) { level = 2; }
                    else if (rBtnHARD.Checked == true) { level = 3; }
                    BTNNewGame_Click(null, null);
                    this.DialogResult = DialogResult.Cancel;
                }
                else
                {
                    if (level == 1) { rBtnEASY.Checked = true; }
                    else if (level == 2) { rBtnMEDIUM.Checked = true; }
                    else if (level == 3) { rBtnHARD.Checked = true; }

                    this.DialogResult = DialogResult.Cancel;
                }
            }
        }

        private void загрузитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Sudoku(*.xml)|*.xml|All files(*.*)|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            else
            {
                System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(save));
                System.IO.StreamReader fileREAD = new System.IO.StreamReader(openFileDialog1.FileName);

                ss = (save)reader.Deserialize(fileREAD);

                View();
                fileREAD.Close();
            }
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (BTNPlayPause.Enabled == true)
            {
                saveFileDialog1.Filter = "Sudoku(*.xml)|*.xml|All files(*.*)|*.*";

                if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                {
                    return;
                }
                else
                {
                    if (rBtnEASY.Checked == true) { level = 1; }
                    else if (rBtnMEDIUM.Checked == true) { level = 2; }
                    else if (rBtnHARD.Checked == true) { level = 3; }

                    save ss = new save(newgeneretion.grid, newgeneretion.gridGame, newgeneretion.fillType, hide, level);
                    System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(typeof(save));
                    var path = saveFileDialog1.FileName;
                    System.IO.FileStream file = System.IO.File.Create(path);
                    writer.Serialize(file, ss);
                    file.Close();
                    MessageBox.Show("Файл сохранен");
                }
            }
            else
            {
                MessageBox.Show("Вы не начали игру!");
            }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (BTNPlayPause.Enabled == true)
            {
                DialogResult dr = MessageBox.Show("Хотите выйти, не сохранив игру?", "Выход", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    Application.Exit();
                }
                else
                {
                    сохранитьToolStripMenuItem_Click(null, null);
                    Application.Exit();
                }
            }
            else
            {
                Application.Exit();
            }

        }

        private void правилаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (BTNPlayPause.Enabled == true && BTNPlayPause.Text == "Пауза")
            {
                tablPN.Visible = false;
                BTNPlayPause.Text = "Продолжить";
                timer.Enabled = false;
                timer.Stop();
            }
            FormRool rool = new FormRool();
            rool.Show();
        }

        private void MusicStop(object sender, EventArgs e)
        {
            sp.Stop();
            pictureBox1.Image = System.Drawing.Image.FromFile("C:\\Users\\Екатерина\\source\\repos\\КУРСОВАЯ ПРОГИ 4 СЕМ\\судоку начало\\иконки\\music-after.png");
            this.pictureBox1.Click += new EventHandler(MusicPlay);
        }

        private void MusicPlay(object sender, EventArgs e)
        {
            sp.Play();
            pictureBox1.Image = System.Drawing.Image.FromFile("C:\\Users\\Екатерина\\source\\repos\\КУРСОВАЯ ПРОГИ 4 СЕМ\\судоку начало\\иконки\\png-clipart-music-icon-music-icon-music-material.png");
            this.pictureBox1.Click += new EventHandler(MusicStop);
        }

    }
}

