using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test_qltn0310.ExamSystem.App.Forms
{
    public class PortalSelectorForm : Form   // 👈 không còn partial nữa
    {
        public PortalSelectorForm()
        {
            this.Text = "Exam Management System - Portal Selector";
            this.Width = 900;
            this.Height = 600;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;

            BuildUI();
        }

        private void BuildUI()
        {
            // Header Title
            Label lblTitle = new Label
            {
                Text = "Exam Management System",
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(220, 20)
            };
            this.Controls.Add(lblTitle);

            // Subtitle
            Label lblSubtitle = new Label
            {
                Text = "VNU University - Hệ thống quản lý thi trực tuyến\nChọn cổng đăng nhập phù hợp với vai trò của bạn",
                Font = new Font("Segoe UI", 10),
                AutoSize = true,
                Location = new Point(250, 80)
            };
            this.Controls.Add(lblSubtitle);

            // FlowLayoutPanel để chứa các card
            FlowLayoutPanel flow = new FlowLayoutPanel
            {
                Location = new Point(50, 150),
                Size = new Size(800, 380),
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true
            };
            this.Controls.Add(flow);

            // 3 Card
            flow.Controls.Add(CreatePortalCard("Admin Portal", "Dành cho quản trị viên hệ thống",
                new string[] { "Quản lý toàn bộ hệ thống", "Quản lý người dùng", "Giám sát kỳ thi" },
                Color.LightBlue, () => OnSelectPortal("admin")));

            flow.Controls.Add(CreatePortalCard("Staff Portal", "Trưởng bộ môn & Giảng viên",
                new string[] { "Quản lý môn học", "Tạo đề thi & câu hỏi", "Báo cáo kết quả" },
                Color.Plum, () => OnSelectPortal("staff")));

            flow.Controls.Add(CreatePortalCard("Student Portal", "Dành cho sinh viên",
                new string[] { "Tham gia thi trực tuyến", "Xem kết quả thi", "Lịch thi của tôi" },
                Color.LightGreen, () => OnSelectPortal("student")));
        }

        private Panel CreatePortalCard(string title, string desc, string[] features, Color btnColor, Action onClick)
{
    Panel card = new Panel
    {
        Width = 250,
        Height = 320,
        BorderStyle = BorderStyle.FixedSingle,
        BackColor = Color.WhiteSmoke,
        Margin = new Padding(20)
    };

    Label lblTitle = new Label
    {
        Text = title,
        Font = new Font("Segoe UI", 14, FontStyle.Bold),
        AutoSize = true,
        Location = new Point(20, 20)
    };
    card.Controls.Add(lblTitle);

    Label lblDesc = new Label
    {
        Text = desc,
        Font = new Font("Segoe UI", 9),
        AutoSize = true,
        Location = new Point(20, 60)
    };
    card.Controls.Add(lblDesc);

    int y = 100;
    foreach (var f in features)
    {
        Label lblFeature = new Label
        {
            Text = "• " + f,
            Font = new Font("Segoe UI", 9),
            AutoSize = true,
            Location = new Point(30, y)
        };
        card.Controls.Add(lblFeature);
        y += 25;
    }

    Button btnLogin = new Button
    {
        Text = "Đăng nhập",
        Width = 200,
        Height = 40,
        BackColor = btnColor,
        Location = new Point(25, 240)
    };
    btnLogin.Click += (s, e) => onClick();
    card.Controls.Add(btnLogin);

    return card;
}


        private void OnSelectPortal(string role)
        {
            if (role == "admin")
            {
                this.Hide(); // ẩn PortalSelectorForm để giao diện gọn

                using (var login = new AdminLoginForm())
                {
                    var result = login.ShowDialog(this);

                    if (result == DialogResult.OK)
                    {
                        // Đăng nhập thành công → mở Dashboard
                        using (var dashboard = new AdminDashboardForm())
                        {
                            dashboard.ShowDialog(this);
                        }
                    }
                    else
                    {
                        // Nếu bấm Back hoặc sai → quay lại PortalSelectorForm
                    }
                }

                this.Show(); // Sau khi Dashboard/ Login đóng → hiện lại portal
            }
        }

    }
}

