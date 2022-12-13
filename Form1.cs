using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml;
using System.IO;
using MySql.Data.MySqlClient;
using System.Reflection;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        Connector db = new Connector();
        DataTable table = new DataTable();
        DataSet dataset = null;
        MySqlDataAdapter adapter = new MySqlDataAdapter();
        MySqlDataReader reader;
        MySqlCommand command;
        int id_arendodatel = 0;
        int index = 0;
        string temp_name = null;
        int index_num = 0;
        string table_name = null;
        public Form1()
        {
            InitializeComponent();
        }


        private void button_user_Click(object sender, EventArgs e)
        {
            dataGridView2.MultiSelect = false; //запретить выделение нескольких строк
            dataGridView2.SelectionMode = DataGridViewSelectionMode.CellSelect; // выделение только одной ячейки

            dataGridView2.Rows.Clear();
            if (id_arendodatel != 0)
            {

                try //Если будет ошибка в программе то сработает catch
                {
                    command = new MySqlCommand("select * from arendator", db.getConnection());

                    command.Connection.Open();
                    adapter.SelectCommand = command;
                    adapter.Fill(table);

                    if (table.Rows.Count > 0)
                    {
                        reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            //заносит строку из бд в строку в программе
                            string temp = Convert.ToString(reader["id_arendator"]) + "!" +
                                           Convert.ToString(reader["FIO"]) + "!" +
                                           Convert.ToString(reader["phone_number"]) + "!" +
                                           Convert.ToString(reader["mail"]) + "!" +
                                                    "удалить";
                            //разделяет строку на слова. Если перед словом встречается ! то делит
                            string[] full = temp.Split(new char[] { '!' });
                            dataGridView2.Rows.Add(full);
                        }

                        command.Connection.Close();
                        reader.Close();
                    }
                    else
                    {
                        MessageBox.Show
                              ("В таблице нет данных. \n Обратитесь в службу поддержки.",
                              "Предупреждение",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Exclamation,
                              MessageBoxDefaultButton.Button1
                              );
                    }

                }
                catch
                {
                    command.Connection.Close();
                    MessageBox.Show
                          ("Ожидайте отклика программы",
                          "Предупреждение",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Exclamation,
                          MessageBoxDefaultButton.Button1
                          );
                }
                panel_users.Visible = true;
            }
            else
            {
                MessageBox.Show
                          ("Необходимо авторизироваться",
                          "Предупреждение",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Exclamation,
                          MessageBoxDefaultButton.Button1
                          );
                panel3.Visible = true;
            }

        }

        private void button_cater_Click(object sender, EventArgs e)
        {
            dataGridView1.MultiSelect = false; //запретить выделение нескольких строк
            dataGridView1.SelectionMode = DataGridViewSelectionMode.CellSelect; // выделение только одной ячейки

            dataGridView1.Rows.Clear(); // очищает коллекцию
            if(id_arendodatel != 0)
            {
                try //Если будет ошибка в программе то сработает catch
                {
                    command = new MySqlCommand("select * from transport", db.getConnection());
                    command.Connection.Open();
                    adapter.SelectCommand = command;
                    adapter.Fill(table);

                    if (table.Rows.Count > 0)
                    {
                        reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            //заносит строку из бд в строку в программе
                            string temp = Convert.ToString(reader["id_transport"]) + "!" +
                                           Convert.ToString(reader["name"]) + "!" +
                                           Convert.ToString(reader["dlina_m"]) + "!" +
                                           Convert.ToString(reader["kolvo_passagir"]) + "!" +
                                           Convert.ToString(reader["zapas_topliva"]) + "!" +
                                           Convert.ToString(reader["speed_chas"]) + "!" +
                                           Convert.ToString(reader["price_chas"]) + "!" +
                                                "удалить";
                            //разделяет строку на слова. Если перед словом встречается ! то делит
                            string[] full = temp.Split(new char[] { '!' });
                            dataGridView1.Rows.Add(full);
                        }

                        command.Connection.Close(); // закрывает подключение
                        reader.Close();
                    }
                    else // если условие не выполняется, выводится это сообщение
                    {
                        MessageBox.Show
                              ("В таблице нет данных. \n Обратитесь в службу поддержки.",
                              "Предупреждение",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Exclamation,
                              MessageBoxDefaultButton.Button1
                              );
                    }

                }
                catch // при ошибке выведет предупреждение
                {
                    command.Connection.Close();
                    MessageBox.Show
                          ("Ожидайте отклика программы",
                          "Предупреждение",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Exclamation,
                          MessageBoxDefaultButton.Button1
                          );
                }
                panel_transport.Visible = true;
            }
            else
            {
                MessageBox.Show
                          ("Необходимо авторизироваться",
                          "Предупреждение",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Exclamation,
                          MessageBoxDefaultButton.Button1
                          );
                panel3.Visible = true;
            }

        }

        private void button_login_Click(object sender, EventArgs e)
        {
            panel3.Visible = true;

        }


        private void button_zakaz_Click(object sender, EventArgs e)
        {
            dataGridView3.MultiSelect = false; //запретить выделение нескольких строк
            dataGridView3.SelectionMode = DataGridViewSelectionMode.CellSelect; // выделение только одной ячейки

            dataGridView3.Rows.Clear();
            if (id_arendodatel != 0)
            {

                //try //Если будет ошибка в программе то сработает catch
                //{
                    command = new MySqlCommand("SELECT id_zakaz, arendator.FIO as arendator, arendodatel.FIO as arendodatel, transport.name, kolvo_time, price, data FROM zakaz, arendator, arendodatel, transport WHERE zakaz.id_arendodatel_zakaz = arendodatel.id_arendodatel and zakaz.id_arendator_zakaz = arendator.id_arendator and zakaz.id_transport_zakaz = transport.id_transport", db.getConnection());
                    command.Connection.Open();
                    adapter.SelectCommand = command;
                    adapter.Fill(table);

                    if (table.Rows.Count > 0)
                    {
                        reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            //заносит строку из бд в строку в программе
                            string temp = Convert.ToString(reader["id_zakaz"]) + "!" +
                                           Convert.ToString(reader["arendator"]) + "!" +
                                           Convert.ToString(reader["arendodatel"]) + "!" +
                                           Convert.ToString(reader["name"]) + "!" +
                                           Convert.ToString(reader["price"]) + "!" +
                                           Convert.ToString(reader["data"]) + "!" +
                                           Convert.ToString(reader["kolvo_time"]) + "!" +
                                                    "удалить";
                            //разделяет строку на слова. Если перед словом встречается ! то делит
                            string[] full = temp.Split(new char[] { '!' });
                            dataGridView3.Rows.Add(full);
                        }

                        command.Connection.Close();
                        reader.Close();
                    panel_zakaz.Visible = true;

                }
                else
                    {
                        MessageBox.Show
                              ("В таблице нет данных. \n Обратитесь в службу поддержки.",
                              "Предупреждение",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Exclamation,
                              MessageBoxDefaultButton.Button1
                              );
                    }

                //}
                //catch
                //{
                //    command.Connection.Close();
                //    MessageBox.Show
                //          ("Ожидайте отклика программы",
                //          "Предупреждение",
                //          MessageBoxButtons.OK,
                //          MessageBoxIcon.Exclamation,
                //          MessageBoxDefaultButton.Button1
                //          );
                //}
            }
            else
            {
                MessageBox.Show
                          ("Необходимо авторизироваться",
                          "Предупреждение",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Exclamation,
                          MessageBoxDefaultButton.Button1
                          );
                panel3.Visible = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Modified && textBox2.Modified)
            {


                string loginUser = textBox1.Text;
                string pasUser = textBox2.Text;
                command = new MySqlCommand("select FIO, id_arendodatel from arendodatel where login = @ul and password = @up", db.getConnection()); // SQL-запрос
                command.Parameters.Add("@ul", MySqlDbType.VarChar).Value = loginUser; // создаем параметры, в которых передаем значения
                command.Parameters.Add("@up", MySqlDbType.VarChar).Value = pasUser;

                adapter.SelectCommand = command; // в адаптер передаем команду
                adapter.Fill(table);

                if (table.Rows.Count > 0) // если адаптер заполняет таблицу, то запускается этот алгоритм
                {
                    command.Connection.Open(); // подключается команда
                    reader = command.ExecuteReader(); // запускается ридер
                    reader.Read(); // считывает данные
                    label3.Text = "Сотрудник: " + Convert.ToString(reader["FIO"]); // заносит ФИО сотрудника
                    id_arendodatel = Convert.ToInt32(reader["id_arendodatel"]); // заносит код арендодателя в переменную
                    command.Connection.Close(); // закрывает подключение
                    panel3.Visible = false; // скрывает панель
                    textBox1.Text = ""; // очищает данные из панели
                    textBox2.Text = "";
                    label19.Text = "";

                }
                else MessageBox.Show("Данного пользователя не существует"); // выводит сообщение
            }
            else 
            {
                label19.Text = "Заполните все поля";
            }
            button_login.Visible = false;
        }

        private void button_add_users_Click(object sender, EventArgs e)
        {
            panel_add_users.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox3.Modified && textBox4.Modified && textBox5.Modified)
                {
                    command = new MySqlCommand("insert into arendator(FIO,phone_number,mail) values(@FIO,@phone_number,@mail)", db.getConnection());
                    command.Parameters.Add("@FIO", MySqlDbType.VarChar).Value = textBox3.Text;
                    command.Parameters.Add("@phone_number", MySqlDbType.VarChar).Value = textBox4.Text;
                    command.Parameters.Add("@mail", MySqlDbType.VarChar).Value = textBox5.Text;
                    adapter.SelectCommand = command;
                    adapter.Fill(table);
                    dataGridView2.Rows.Clear();

                        command = new MySqlCommand("select * from arendator", db.getConnection());
                        command.Connection.Open();
                        adapter.SelectCommand = command;
                        adapter.Fill(table);

                        if (table.Rows.Count > 0)
                        {
                            reader = command.ExecuteReader();

                            while (reader.Read())
                            {
                                string temp = Convert.ToString(reader["id_arendator"]) + "!" +
                                               Convert.ToString(reader["FIO"]) + "!" +
                                               Convert.ToString(reader["phone_number"]) + "!" +
                                               Convert.ToString(reader["mail"]) + "!" +
                                                "удалить";
                                string[] full = temp.Split(new char[] { '!' });
                                dataGridView2.Rows.Add(full);
                            }

                            command.Connection.Close();
                            reader.Close();
                        }
                    panel_add_users.Visible = false;

                }
                else
                {
                    MessageBox.Show
                    ("Заполните все поля",
                    "Предупреждение",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1
                    );

                }

            }
            catch
            {
                 MessageBox.Show
                ("Введены неверные данные. \n Проверьте правильность заполнения",
                "Предупреждение",
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1
                );
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox6.Modified && textBox7.Modified && textBox8.Modified && textBox9.Modified && textBox10.Modified && textBox11.Modified)
                {
                    command = new MySqlCommand("insert into transport(name,dlina_m,kolvo_passagir,zapas_topliva,speed_chas,price_chas) values(@name,@dlina_m,@kolvo_passagir,@zapas_topliva,@speed_chas,@price_chas)", db.getConnection());
                    command.Parameters.Add("@name", MySqlDbType.VarChar).Value = textBox6.Text;
                    command.Parameters.Add("@dlina_m", MySqlDbType.Int32).Value = textBox7.Text;
                    command.Parameters.Add("@kolvo_passagir", MySqlDbType.Int32).Value = textBox8.Text;
                    command.Parameters.Add("@zapas_topliva", MySqlDbType.Int32).Value = textBox9.Text;
                    command.Parameters.Add("@speed_chas", MySqlDbType.Int32).Value = textBox10.Text;
                    command.Parameters.Add("@price_chas", MySqlDbType.Int32).Value = textBox11.Text;
                    adapter.SelectCommand = command;
                    adapter.Fill(table);

                    dataGridView1.Rows.Clear();
                        command = new MySqlCommand("select * from transport", db.getConnection());
                        command.Connection.Open();
                        adapter.SelectCommand = command;
                        adapter.Fill(table);

                        if (table.Rows.Count > 0)
                        {
                            reader = command.ExecuteReader();

                            while (reader.Read())
                            {
                                string temp = Convert.ToString(reader["id_transport"]) + "!" +
                                               Convert.ToString(reader["name"]) + "!" +
                                               Convert.ToString(reader["dlina_m"]) + "!" +
                                               Convert.ToString(reader["kolvo_passagir"]) + "!" +
                                               Convert.ToString(reader["zapas_topliva"]) + "!" +
                                               Convert.ToString(reader["speed_chas"]) + "!" +
                                               Convert.ToString(reader["price_chas"]) + "!" +
                                                "удалить";
                            string[] full = temp.Split(new char[] { '!' });
                                dataGridView1.Rows.Add(full);
                            }
                            command.Connection.Close();
                            reader.Close();
                        }
                    panel_add_transport.Visible = false;
                }
                else
                {
                    MessageBox.Show
                    ("Заполните все поля",
                    "Предупреждение",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1
                    );

                }

            }
            catch
            {
                MessageBox.Show
               ("Введены неверные данные. \n Проверте правильность заполнения",
               "Предупреждение",
               MessageBoxButtons.OK,
               MessageBoxIcon.Exclamation,
               MessageBoxDefaultButton.Button1
               );

            }

        }

        private void button_add_transport_Click(object sender, EventArgs e)
        {
            panel_add_transport.Visible = true;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string value = null;

            int index = dataGridView1.CurrentRow.Index; //получение индекса строки
            index_num = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString()); //получение значения первого столбца строки
            var value_cell = dataGridView1.CurrentCell.Value; //для определения значения в ячейке
            value = value_cell.ToString();

            if (value == "удалить")
            {
                DialogResult result = MessageBox.Show
                         ("Вы действительно хотите удалить строку?",
                         "Подтверждение",
                         MessageBoxButtons.YesNo,
                         MessageBoxIcon.Question,
                         MessageBoxDefaultButton.Button1
                         );
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        dataGridView1.Rows.RemoveAt(index);
                                command = new MySqlCommand("delete from transport where id_transport = @id LIMIT 1;", db.getConnection());
                        command.Parameters.Add("@id", MySqlDbType.VarChar).Value = index_num;

                        command.Connection.Open();
                        adapter.SelectCommand = command;
                        adapter.Fill(table);
                        command.Connection.Close();
                    }
                    catch
                    {
                        MessageBox.Show
                        ("Данную запись временно невозможно удалить. \n Обратитесь в службу поддержки.",
                        "Ошибка",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning,
                        MessageBoxDefaultButton.Button1
                        );
                    }
                }
                if (result == DialogResult.No)
                {
                    dataGridView1.ClearSelection();
                }
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string value = null;

            int index = dataGridView2.CurrentRow.Index; //получение индекса строки
            index_num = int.Parse(dataGridView2.CurrentRow.Cells[0].Value.ToString()); //получение значения первого столбца строки
            var value_cell = dataGridView2.CurrentCell.Value; //для определения значения в ячейке
            value = value_cell.ToString();

            if (value == "удалить")
            {
                DialogResult result = MessageBox.Show
                         ("Вы действительно хотите удалить строку?",
                         "Подтверждение",
                         MessageBoxButtons.YesNo,
                         MessageBoxIcon.Question,
                         MessageBoxDefaultButton.Button1
                         );
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        dataGridView2.Rows.RemoveAt(index);
                        command = new MySqlCommand("delete from arendator where id_arendator = @id LIMIT 1;", db.getConnection());
                        command.Parameters.Add("@id", MySqlDbType.VarChar).Value = index_num;

                        command.Connection.Open();
                        adapter.SelectCommand = command;
                        adapter.Fill(table);
                        command.Connection.Close();
                    }
                    catch
                    {
                        MessageBox.Show
                        ("Данную запись временно невозможно удалить. \n Обратитесь в службу поддержки.",
                        "Ошибка",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning,
                        MessageBoxDefaultButton.Button1
                        );
                    }
                }
                if (result == DialogResult.No)
                {
                    dataGridView1.ClearSelection();
                }
            }

        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string value = null;

            int index = dataGridView3.CurrentRow.Index; //получение индекса строки
            index_num = int.Parse(dataGridView3.CurrentRow.Cells[0].Value.ToString()); //получение значения первого столбца строки
            var value_cell = dataGridView3.CurrentCell.Value; //для определения значения в ячейке
            value = value_cell.ToString();

            if (value == "удалить")
            {
                DialogResult result = MessageBox.Show
                         ("Вы действительно хотите удалить строку?",
                         "Подтверждение",
                         MessageBoxButtons.YesNo,
                         MessageBoxIcon.Question,
                         MessageBoxDefaultButton.Button1
                         );
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        dataGridView3.Rows.RemoveAt(index);
                        command = new MySqlCommand("delete from zakaz where id_zakaz = @id LIMIT 1;", db.getConnection());
                        command.Parameters.Add("@id", MySqlDbType.VarChar).Value = index_num;

                        command.Connection.Open();
                        adapter.SelectCommand = command;
                        adapter.Fill(table);
                        command.Connection.Close();
                    }
                    catch
                    {
                        MessageBox.Show
                        ("Данную запись временно невозможно удалить. \n Обратитесь в службу поддержки.",
                        "Ошибка",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning,
                        MessageBoxDefaultButton.Button1
                        );
                    }
                }
                if (result == DialogResult.No)
                {
                    dataGridView1.ClearSelection();
                }
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox12.Modified && textBox13.Modified && textBox14.Modified && textBox15.Modified && textBox16.Modified)
                {
                    command = new MySqlCommand("insert into zakaz(id_arendator_zakaz,id_arendodatel_zakaz,id_transport_zakaz, price, data, kolvo_time) values(@id_arendator_zakaz,@id_arendodatel_zakaz,@id_transport_zakaz, @price, @data, @kolvo_time)", db.getConnection());
                    command.Parameters.Add("@id_arendator_zakaz", MySqlDbType.Int32).Value = Convert.ToInt32(textBox13.Text);
                    command.Parameters.Add("@id_arendodatel_zakaz", MySqlDbType.Int32).Value = Convert.ToInt32(textBox12.Text);
                    command.Parameters.Add("@id_transport_zakaz", MySqlDbType.Int32).Value = Convert.ToInt32(textBox14.Text);
                    command.Parameters.Add("@price", MySqlDbType.Int32).Value = Convert.ToInt32(textBox16.Text);
                    command.Parameters.Add("@data", MySqlDbType.VarChar).Value = textBox17.Text;
                    command.Parameters.Add("@kolvo_time", MySqlDbType.Int32).Value = Convert.ToInt32(textBox15.Text);
                    adapter.SelectCommand = command;
                    adapter.Fill(table);
                    dataGridView3.Rows.Clear();
                    //Если будет ошибка в программе то сработает catch

                    command = new MySqlCommand("SELECT id_zakaz, arendator.FIO as arendator, arendodatel.FIO as arendodatel, transport.name, kolvo_time, price, data FROM zakaz, arendator, arendodatel, transport WHERE zakaz.id_arendodatel_zakaz = arendodatel.id_arendodatel and zakaz.id_arendator_zakaz = arendator.id_arendator and zakaz.id_transport_zakaz = transport.id_transport", db.getConnection());
                    command.Connection.Open();
                    adapter.SelectCommand = command;
                    adapter.Fill(table);

                    if (table.Rows.Count > 0)
                    {
                        reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            //заносит строку из бд в строку в программе
                            string temp = Convert.ToString(reader["id_zakaz"]) + "!" +
                                           Convert.ToString(reader["arendator"]) + "!" +
                                           Convert.ToString(reader["arendodatel"]) + "!" +
                                           Convert.ToString(reader["name"]) + "!" +
                                           Convert.ToString(reader["price"]) + "!" +
                                           Convert.ToString(reader["data"]) + "!" +
                                           Convert.ToString(reader["kolvo_time"]) + "!" +
                                                    "удалить";
                            //разделяет строку на слова. Если перед словом встречается ! то делит
                            string[] full = temp.Split(new char[] { '!' });
                            dataGridView3.Rows.Add(full);
                        }

                        command.Connection.Close();
                        reader.Close();
                    }
                    else
                    {
                        MessageBox.Show
                              ("В таблице нет данных. \n Обратитесь в службу поддержки.",
                              "Предупреждение",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Exclamation,
                              MessageBoxDefaultButton.Button1
                              );
                    }

                }
            }
            catch
            {
                command.Connection.Close();
                MessageBox.Show
                      ("Ожидайте отклика программы",
                      "Предупреждение",
                      MessageBoxButtons.OK,
                      MessageBoxIcon.Exclamation,
                      MessageBoxDefaultButton.Button1
                      );
            }
            panel_add_zakaz.Visible = false;

        }

        private void button_add_zakaz_Click(object sender, EventArgs e)
        {
            panel_add_zakaz.Visible = true;
            textBox17.Text = DateTime.Now.ToString("yy-MM-dd hh:mm:ss");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            panel_add_zakaz.Visible = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            panel_add_transport.Visible = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            panel_add_users.Visible = false;
        }

        private void button_change_transport_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Visible)
            {
                table_name = "transport";
                try
                {


                    index = dataGridView1.CurrentRow.Index;
                    var name_column = dataGridView1.CurrentCell.OwningColumn.Name; //для определения названия столбца
                    var value_cell = dataGridView1.CurrentCell.Value; //для определения значения в ячейке

                    temp_name = name_column.ToString(); //заносим в переменную
                    string[] item_user = new string[dataGridView1.ColumnCount];
                    index_num = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());

                    for (int i = 0; i < item_user.Length; i++)
                    {
                        item_user[i] = (string)dataGridView1.Rows[index].Cells[i].Value;
                    }
                    textBox19.Text = value_cell.ToString();
                    textBox18.Text = "Код: " + item_user[0] + ", Имя: " + item_user[1];
                    panel2.Visible = true;

                }
                catch
                {

                    MessageBox.Show
                        ("Строка не выделена.  \n Выделите строку и попробуйте снова.",
                        "Ошибка",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1
                        );

                }
            }
        }

        private void button_change_users_Click(object sender, EventArgs e)
        {
            if (dataGridView2.Visible)
            {
                table_name = "arendator";

                try
                {


                    index = dataGridView2.CurrentRow.Index;
                    var name_column = dataGridView2.CurrentCell.OwningColumn.Name; //для определения названия столбца
                    var value_cell = dataGridView2.CurrentCell.Value; //для определения значения в ячейке

                    temp_name = name_column.ToString(); //заносим в переменную
                    string[] item_user = new string[dataGridView2.ColumnCount];

                    for (int i = 0; i < item_user.Length; i++)
                    {
                        item_user[i] = (string)dataGridView2.Rows[index].Cells[i].Value;
                    }
                    textBox19.Text = value_cell.ToString();
                    textBox18.Text = "Код: " + item_user[0] + ", Имя: " + item_user[1];
                    panel2.Visible = true;

                }
                catch
                {

                    MessageBox.Show
                        ("Строка не выделена.  \n Выделите строку и попробуйте снова.",
                        "Ошибка",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1
                        );

                }
            }
        }

        private void button_change_zakaz_Click(object sender, EventArgs e)
        {
            if (dataGridView3.Visible)
            {
                table_name = "zakaz";

                try
                {


                    index = dataGridView3.CurrentRow.Index;
                    var name_column = dataGridView3.CurrentCell.OwningColumn.Name; //для определения названия столбца
                    var value_cell = dataGridView3.CurrentCell.Value; //для определения значения в ячейке

                    temp_name = name_column.ToString(); //заносим в переменную
                    string[] item_user = new string[dataGridView3.ColumnCount];

                    for (int i = 0; i < item_user.Length; i++)
                    {
                        item_user[i] = (string)dataGridView3.Rows[index].Cells[i].Value;
                    }
                    textBox19.Text = value_cell.ToString();
                    textBox18.Text = "Код: " + item_user[0] + ", Имя: " + item_user[1];
                    panel2.Visible = true;

                }
                catch
                {

                    MessageBox.Show
                        ("Строка не выделена.  \n Выделите строку и попробуйте снова.",
                        "Ошибка",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1
                        );

                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string zapros = null;
            if (textBox20.Text == "" || textBox19.Text == "")
            {
                MessageBox.Show("not text");
            }
            else
            {
                DialogResult result = MessageBox.Show
                        ("Вы действительно хотите изменить запись?",
                        "Подтверждение",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button1
                        );
                if (result == DialogResult.Yes)
                {
                    //try
                    //{


                        switch (table_name)
                        {
                            case "transport":
                                zapros = "update transport set " + temp_name + " = " + "'" + textBox20.Text + "'" + " where id_transport=" + index_num.ToString();
                                break;
                            case "arendator":
                                zapros = "update arendator set " + temp_name + " = " + "'" + textBox20.Text + "'" + " where id_arendator=" + index_num.ToString();
                                break;
                            case "zakaz":
                                zapros = "update zakaz set " + temp_name + " = " + "'" + textBox20.Text + "'" + " where id_zakaz=" + index_num.ToString();
                                break;

                        }
                        command = new MySqlCommand(zapros, db.getConnection());
                        command.Connection.Open();
                        adapter.SelectCommand = command;
                        adapter.Fill(table);

                        command.Connection.Close();
                    panel2.Visible = false;
                    //}
                    //catch (Exception)
                    //{
                    //    MessageBox.Show
                    //    ("Введены неверные данные",
                    //    "Ошибка",
                    //    MessageBoxButtons.OK,
                    //    MessageBoxIcon.Error,
                    //    MessageBoxDefaultButton.Button1
                    //    );
                    //}
                }
                if (result == DialogResult.No)
                {
                    textBox2.Text = "";
                    panel2.Visible = false;
                }

            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            //Эта строка кода создает текстовый файл для экспорта данных
            System.IO.StreamWriter file = new System.IO.StreamWriter(@"G:\highraft\WindowsFormsApp1\Отчёты\transport.txt");
            try
            {
                string sLine = "";
                //Этот цикл for повторяется через каждую строку в таблице
                for (int r = 0; r <= dataGridView1.Rows.Count - 1; r++)
                {
                    //Это для цикла, проходящего через каждый столбец, и номер строки
                    //передается из цикла for выше
                    for (int c = 0; c <= dataGridView1.Columns.Count - 2; c++)
                    {
                        sLine = sLine + dataGridView1.Rows[r].Cells[c].Value;
                        if (c != dataGridView1.Columns.Count - 2)
                        {
                            //Запятая добавляется в качестве разделителя текста для того, чтобы
                            //для разделения каждого поля в текстовом файле.
                            sLine = sLine + " | ";
                        }
                    }
                    //Экспортированный текст записывается в текстовый файл по одной строке за раз.
                    file.WriteLine(sLine);
                    sLine = "";
                }

                file.Close();
                System.Windows.Forms.MessageBox.Show("Успешно!", "Program Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (System.Exception err)
            {
                System.Windows.Forms.MessageBox.Show(err.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                file.Close();
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            //Эта строка кода создает текстовый файл для экспорта данных
            System.IO.StreamWriter file = new System.IO.StreamWriter(@"G:\highraft\WindowsFormsApp1\Отчёты\arendators.txt");
            try
            {
                string sLine = "";
                //Этот цикл for повторяется через каждую строку в таблице
                for (int r = 0; r <= dataGridView2.Rows.Count - 1; r++)
                {
                    //Это для цикла, проходящего через каждый столбец, и номер строки
                    //передается из цикла for выше
                    for (int c = 0; c <= dataGridView2.Columns.Count - 2; c++)
                    {
                        sLine = sLine + dataGridView2.Rows[r].Cells[c].Value;
                        if (c != dataGridView2.Columns.Count - 2)
                        {
                            //Запятая добавляется в качестве разделителя текста для того, чтобы
                            //для разделения каждого поля в текстовом файле.
                            sLine = sLine + " | ";
                        }
                    }
                    //Экспортированный текст записывается в текстовый файл по одной строке за раз.
                    file.WriteLine(sLine);
                    sLine = "";
                }

                file.Close();
                System.Windows.Forms.MessageBox.Show("Успешно!", "Program Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (System.Exception err)
            {
                System.Windows.Forms.MessageBox.Show(err.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                file.Close();
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            //Эта строка кода создает текстовый файл для экспорта данных
            System.IO.StreamWriter file = new System.IO.StreamWriter(@"G:\highraft\WindowsFormsApp1\Отчёты\zakaz.txt");
            try
            {
                string sLine = "";
                //Этот цикл for повторяется через каждую строку в таблице
                for (int r = 0; r <= dataGridView3.Rows.Count - 1; r++)
                {
                    //Это для цикла, проходящего через каждый столбец, и номер строки
                    //передается из цикла for выше
                    for (int c = 0; c <= dataGridView3.Columns.Count - 2; c++)
                    {
                        sLine = sLine + dataGridView3.Rows[r].Cells[c].Value;
                        if (c != dataGridView3.Columns.Count - 2)
                        {
                            //Запятая добавляется в качестве разделителя текста для того, чтобы
                            //для разделения каждого поля в текстовом файле.
                            sLine = sLine + " | ";
                        }
                    }
                    //Экспортированный текст записывается в текстовый файл по одной строке за раз.
                    file.WriteLine(sLine);
                    sLine = "";
                }

                file.Close();
                System.Windows.Forms.MessageBox.Show("Успешно!", "Program Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (System.Exception err)
            {
                System.Windows.Forms.MessageBox.Show(err.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                file.Close();
            }
        }
    }
}
