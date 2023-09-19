using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsAppHW20230624
{    
    public partial class Child : Form
    {
        // ссылка на родителя
        Parent parent = null;

        // делегат для передачи текста из дочерней формы в родительскую
        public delegate void ChildTextChangedEventHandlerWithDelegate(string newText);

        // событие для делегата
        public event ChildTextChangedEventHandlerWithDelegate ChildTextChangedWithDelegate;

        public Child()
        {
            InitializeComponent();
        }

        // Конструктор, принимающий родительскую форму
        public Child(Parent parent)
        {
            InitializeComponent();
            this.parent = parent;
            textBox1.TextChanged += ChildTextBox1_TextChanged; 
            parent.ParentTextBoxChangedWithDelegate += ParentTextBox1ChangedWithDelegate; // Подписываемся на событие ParentTextChangedWithDelegate родительской формы
        }

        // Обработчик события изменения текста в textBox1 дочерней формы
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string childText = textBox1.Text;
            parent.ParentTextBox1Text = childText;
            ChildTextChangedWithDelegate?.Invoke(childText); // Вызываем событие ChildTextChangedWithDelegate и передаем в него текущий текст
        }

        // Обработчик события изменения текста в textBox1 дочерней формы
        private void ChildTextBox1_TextChanged(object? sender, EventArgs e)
        {
            string newText = textBox1.Text;
            ChildTextChangedWithDelegate?.Invoke(newText); // Вызываем событие ChildTextChangedWithDelegate и передаем в него новый текст
        }

        // Обработчик события изменения текста в родительской форме
        private void ParentTextBox1ChangedWithDelegate(string parentText)
        {
            textBox1.Text = parentText;
        }                
    }
}
