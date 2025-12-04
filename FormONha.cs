using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
	public partial class Form1 : Form
	{
		DataUtils data = new DataUtils();
		public Form1()
		{
			InitializeComponent();
			loadData();
		}

		private void label1_Click(object sender, EventArgs e)
		{

		}

		public void loadData()
		{
			data.loadData(dataGridView1);
		}

		public NhanVien getNhanVien()
		{
			NhanVien nhanVien = new NhanVien();

			nhanVien.maNhanVien = txtMaNhanVien.Text;
			nhanVien.ngayLamThem = txtNgayLamViec.Text;
			nhanVien.trangThai = txtTrangThai.Text;
			nhanVien.loaiLamThem = txtLoaiLamThem.Text;
			nhanVien.soGio = short.Parse(txtSoGio.Text);

			return nhanVien;

		}

		private void btnThem_Click(object sender, EventArgs e)
		{
			data.them(getNhanVien());
			loadData();
		}

		private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			int index = e.RowIndex;
			if (index >= 0) { 
			     txtNgayLamViec.Text = dataGridView1.Rows[index].Cells[0].Value.ToString();
				txtMaNhanVien.Text = dataGridView1.Rows[index].Cells[1].Value.ToString();
				txtLoaiLamThem.Text = dataGridView1.Rows[index].Cells[2].Value.ToString();
				txtSoGio.Text = dataGridView1.Rows[index].Cells[3].Value.ToString();
				txtTrangThai.Text = dataGridView1.Rows[index].Cells[4].Value.ToString();
			}
		}

		private void btnXoa_Click(object sender, EventArgs e)
		{
			data.xoa(getNhanVien());
			loadData();
		}

		private void btnSua_Click(object sender, EventArgs e)
		{
			data.sua(getNhanVien());
			loadData();
		}
	}
}
