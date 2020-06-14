using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab5
{
    public enum direction : byte { Up, Right, Down, Left };
    public partial class Form1 : Form
    {
        Cockroach workCockroach;//рабочий Таракан - активный Таракан, который будет выполнять алгоритм
        PictureBox workpb;//рабочее поле PictureBox - поле на котрой будет рабочий Таракан
        List<Cockroach> LC;//Список для хранения созданных Тараканов
        List<PictureBox> PB;//Список для хранения созданных объектов PictureBox
        int AlgStep; //текущая комманда
        List<PictureBox> ListWorkPB;
        List<Cockroach> ListWorkCockroach;
        Cockroach cockroach;
        int xx, yy;
        bool check = false;
        Random rnd = new Random();
        public Form1()
        {
            InitializeComponent();
            LC = new List<Cockroach>();
            PB = new List<PictureBox>();
            ListWorkPB = new List<PictureBox>();
            ListWorkCockroach = new List<Cockroach>();
        }
        public void RePaint(Cockroach cr, PictureBox p)
        {
            if (check == false)
            {
                p.Bounds = new Rectangle(xx, yy, cr.Image.Width, cr.Image.Height);//создание новых границ изображения для PictureBox
                cr.X = xx;
                cr.Y = yy;
            }
            else
            {
                p.Bounds = new Rectangle(cr.X, cr.Y, cr.Image.Width, cr.Image.Height);//создание новых границ изображения для PictureBox
            }
            p.Image = cr.Image;
        }

        private void NewBtn_Click(object sender, EventArgs e)
        {
            int CockroachNumber = rnd.Next(1, 3);
            if (CockroachNumber == 1)
            {
                cockroach = new Cockroach(new Bitmap("cockroach1.jpg"));
                cockroach.Image.Tag = "1";
            }
            else if (CockroachNumber == 2)
            {
                cockroach = new Cockroach(new Bitmap("cockroach2.jpg"));
                cockroach.Image.Tag = "2";
            }
            else
            {
                cockroach = new Cockroach(new Bitmap("cockroach3.jpg"));
                cockroach.Image.Tag = "3";
            }
            PictureBox p = new PictureBox();
            Show(cockroach, p, Field);
            p.MouseMove += new MouseEventHandler(IMouseMove);
            p.MouseDown += new MouseEventHandler(IMouseDown);
            PB.Add(p);
            LC.Add(cockroach);
            ClearWorkItems();
        }
        private void IMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (Form.ModifierKeys != Keys.Control)
                {
                    ClearWorkItems();
                }

                int k = PB.IndexOf(sender as PictureBox);//запоминаем номер нажатого компонента PictureBox
                workpb = sender as PictureBox;//объявляем его рабочим
                workCockroach = LC[k];//по найденному номеру находим Таракана в списке
                if (!ListWorkPB.Contains(workpb))
                {
                    ListWorkCockroach.Add(workCockroach);
                    ListWorkPB.Add(workpb);
                }
            }
            else if (e.Button == MouseButtons.Right) // смена образа на пкм
            {
                ClearWorkItems();
                int k = PB.IndexOf(sender as PictureBox);
                workpb = sender as PictureBox;
                xx = workpb.Location.X;
                yy = workpb.Location.Y;
                if ((LC[k].Image.Tag).ToString() == "1")
                {
                    LC[k] = new Cockroach(new Bitmap("cockroach2.jpg"));
                    LC[k].Image.Tag = "2";
                }
                else if ((LC[k].Image.Tag).ToString() == "2")
                {
                    LC[k] = new Cockroach(new Bitmap("cockroach3.jpg"));
                    LC[k].Image.Tag = "3";
                }
                else
                {
                    LC[k] = new Cockroach(new Bitmap("cockroach1.jpg"));
                    LC[k].Image.Tag = "1";
                }
                ListWorkPB.Add(workpb);
                ListWorkCockroach.Add(LC[k]);
                check = false;
                RePaint(LC[k], PB[k]);
            }
        }
        private void IMouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                PictureBox picture = sender as PictureBox;
                picture.Tag = new Point(e.X, e.Y);//запоминаем координаты мыши на момент начала перетаскивания
                picture.DoDragDrop(sender, DragDropEffects.Move);//начинаем перетаскивание ЧЕГО и с КАКИМ ЭФФЕКТОМ
            }
        }

        private void Field_DragDrop(object sender, DragEventArgs e)
        {
            if (Form.ModifierKeys == Keys.Control)
            {
                return;
            }
            //извлекаем PictureBox
            PictureBox picture = (PictureBox)e.Data.GetData(typeof(PictureBox));
            Panel panel = sender as Panel;
            //получаем клиентские координаты в момент отпускания кнопки
            Point pointDrop = panel.PointToClient(new Point(e.X, e.Y));
            //извлекаем клиентские координаты мыши в момент начала перетскивания
            Point pointDrag = (Point)picture.Tag;
            //вычисляем и устанавливаем Location для PictureBox в Panel
            picture.Location = pointDrop;
            //устанавливаем координаты для X и Y для рабочего таракана
            workCockroach.X = picture.Location.X;
            workCockroach.Y = picture.Location.Y;
            picture.Parent = panel;
        }

        private void Field_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(PictureBox)))
            {
                e.Effect = DragDropEffects.Move;
            }
        }

        private void UpBtn_Click(object sender, EventArgs e)
        {
            Algorithm.Items.Add((sender as Button).Text);
        }

        private void RightBtn_Click(object sender, EventArgs e)
        {
            Algorithm.Items.Add((sender as Button).Text);
        }

        private void LeftBtn_Click(object sender, EventArgs e)
        {
            Algorithm.Items.Add((sender as Button).Text);
        }

        private void DownBtn_Click(object sender, EventArgs e)
        {
            Algorithm.Items.Add((sender as Button).Text);
        }

        private void StepBtn_Click(object sender, EventArgs e)
        {
            Algorithm.Items.Add((sender as Button).Text);
        }

        private void timerAlgorithm_Tick(object sender, EventArgs e)
        {
            if (AlgStep == Algorithm.Items.Count) //конец алгоритма
            {
                timerAlgorithm.Enabled = false; //выключение таймера
            }
            else//выполнение команды из списка
            {
                for (int i = 0; i < ListWorkCockroach.Count; ++i)
                {
                    string s = (string)Algorithm.Items[AlgStep];
                    Algorithm.SetSelected(AlgStep, true);
                    if (s == "Up") ListWorkCockroach[i].ChangeTrend(s);
                    if (s == "Right") ListWorkCockroach[i].ChangeTrend(s);
                    if (s == "Left") ListWorkCockroach[i].ChangeTrend(s);
                    if (s == "Down") ListWorkCockroach[i].ChangeTrend(s);
                    if (s == "Step") ListWorkCockroach[i].Step();
                    check = true;
                    RePaint(ListWorkCockroach[i], ListWorkPB[i]);
                }
                AlgStep++;
            }
        }

        private void ClearBtn_Click(object sender, EventArgs e)
        {
            Algorithm.Items.Clear();
        }

        private void RunBtn_Click(object sender, EventArgs e)
        {
            timerAlgorithm.Enabled = true;
            AlgStep = 0;
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            while (ListWorkPB.Count > 0)
            {
                ListWorkPB[0].Dispose();
                ListWorkPB.RemoveAt(0);
            }
            if (workpb != null) workpb.Dispose();
            ClearWorkItems();
        }

        private void ClearWorkItems()
        {
            ListWorkCockroach.Clear();
            ListWorkPB.Clear();
        }
        public void Show(Cockroach c, PictureBox p, Panel owner)
        {
            check = true;
            RePaint(c, p);
            owner.Controls.Add(p);// добавляем PictureBox к элементу Panel
        }
    }
}
