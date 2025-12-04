using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chua_Bai_TX2
{
    public partial class Form1 : Form
    {
        DataUtils du = new DataUtils();
        public Form1()
        {
            InitializeComponent();
            hienThi();
        }

        public void hienThi()
        {
            du.hienThiDGV(dgvNhanVien);
        }

        public NhanVien getNhanVien()
        {
            NhanVien nv = new NhanVien();
            nv.maNhanVien = txtMaNhanVien.Text;
            nv.ngayLamThem = txtNgayLamViec.Text;
            nv.soGio = short.Parse(txtSoGio.Text);
            nv.trangThai = txtTrangThai.Text;
            nv.loaiLamThem = txtLoaiLamThem.Text;
            return nv;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            du.them(getNhanVien());
            hienThi();
        }

        private void dgvNhanVien_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int index = e.RowIndex;
            if( index >= 0)
            {
                txtNgayLamViec.Text = dgvNhanVien.Rows[index].Cells[0].Value.ToString();
                txtMaNhanVien.Text = dgvNhanVien.Rows[index].Cells[1].Value.ToString();
                txtLoaiLamThem.Text = dgvNhanVien.Rows[index].Cells[2].Value.ToString();
                txtSoGio.Text = dgvNhanVien.Rows[index].Cells[3].Value.ToString();
                txtTrangThai.Text = dgvNhanVien.Rows[index].Cells[4].Value.ToString();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            du.xoa(getNhanVien());
            hienThi();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            du.tim(txtNgayLamViec.Text, dgvNhanVien);
        }
    }
}
