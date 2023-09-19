namespace WinFormsAppHW20230624
{
    public partial class Parent : Form
    {
        // делегат для создания события, для передачи текста из родительской формы в дочернюю форму
        public delegate void ParentTextBoxChangedEventHandlerWithDelegate(string parentText);

        // событие этого делегата
        public event ParentTextBoxChangedEventHandlerWithDelegate ParentTextBoxChangedWithDelegate;

        // даём доступ к полю textBox1.Text родиельской формы
        public string ParentTextBox1Text
        {
            get { return textBox1.Text; }
            set { textBox1.Text = value; }
        }

        public Child Child { get; set; }
        public Parent()
        {
            InitializeComponent();

            // Получаем размер экрана
            Screen screen = Screen.FromControl(this);
            int screenWidth = screen.Bounds.Width;
            int screenHeight = screen.Bounds.Height;

            // Устанавливаем начальное положение родительской формы чуть левее от центра
            int parentX = (screenWidth - Width) / 4; // 1/4 ширины экрана
            int parentY = (screenHeight - Height) / 2; // по центру по вертикали
            Location = new Point(parentX, parentY);

            // Создаем и отображаем дочернюю форму
            Child = new Child(this);
            Child.StartPosition = FormStartPosition.Manual;
            Child.Location=new Point(parentX +Width, parentY);
            Child.Show();

            textBox1.TextChanged += ParentTextBox1TextChanged; // устанавливаем обработчик события TextChanged для textBox1
        }

        // при изменениях в textBox1 вызывается событие ддя делегата
        private void ParentTextBox1TextChanged(object? sender, EventArgs e)
        {
            string parentText = textBox1.Text;
            ParentTextBoxChangedWithDelegate?.Invoke(parentText);
        }

        private void Parent_Load(object sender, EventArgs e) {}
        private void textBox1_TextChanged(object sender, EventArgs e) {}
    }
}