using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace WindowsFormsApp1
{
	internal class DataUtils
	{
		static string filePath = "D:\\TichHop\\WindowsFormsApp1\\WindowsFormsApp1\\DangKyLamThem.xml";
		static XmlDocument doc = new XmlDocument();
		static XmlElement root;

		public DataUtils()
		{
			doc.Load(filePath);
			root = doc.DocumentElement;

		}

		public void loadData(DataGridView dataGridView)
		{
			dataGridView.Rows.Clear();
			int index = 0;
			XmlNodeList ngayLamThems = doc.SelectNodes("/DangKyLamThem/NgayLamThem");
			foreach (XmlNode nlt in ngayLamThems)
			{
				XmlNodeList nhanVien = nlt.SelectNodes("NhanVien");
				foreach (XmlNode nv in nhanVien)
				{
					dataGridView.Rows.Add();
					dataGridView.Rows[index].Cells[0].Value = nlt.SelectSingleNode("@Ngay").InnerText;
					dataGridView.Rows[index].Cells[1].Value = nv.SelectSingleNode("@Ma").InnerText;
					dataGridView.Rows[index].Cells[2].Value = nv.SelectSingleNode("LoaiLamThem").InnerText;
					dataGridView.Rows[index].Cells[3].Value = nv.SelectSingleNode("SoGio").InnerText;
					dataGridView.Rows[index].Cells[4].Value = nv.SelectSingleNode("TrangThai").InnerText;
					index++;
				}
			}
		}
		public bool kiemTraNhanVien(NhanVien nhanVien)
		{
			XmlNode nv = doc.SelectSingleNode("/DangKyLamThem/NgayLamThem[@Ngay='" + nhanVien.ngayLamThem + "']");
			if(nv == null)
			{
				return false;
			}
			XmlNode maNv = nv.SelectSingleNode("NhanVien[@Ma = '" + nhanVien.maNhanVien + "']");

			if (maNv != null)
			{
				return true;
			}
			return false;
		}

		public bool them(NhanVien x)
		{
			if (kiemTraNhanVien(x))
			{
				MessageBox.Show("Thông tin đã tồn tại");
				return true;
			}
			XmlNode ngayLamThem = doc.SelectSingleNode("/DangKyLamThem/NgayLamThem[@Ngay = '" + x.ngayLamThem + "']");
			if (ngayLamThem != null) {
				XmlElement nhanVien = doc.CreateElement("NhanVien");

				XmlAttribute ma = doc.CreateAttribute("Ma");
				ma.InnerText = x.maNhanVien;
				nhanVien.Attributes.Append(ma);

				XmlElement loaiLamThem = doc.CreateElement("LoaiLamThem");
				loaiLamThem.InnerText = x.loaiLamThem;
				nhanVien.AppendChild(loaiLamThem);

				XmlElement soGio  = doc.CreateElement("SoGio");
				soGio.InnerText = x.soGio.ToString();
				nhanVien.AppendChild(soGio);

				XmlElement trangThai = doc.CreateElement("TrangThai");
				trangThai.InnerText = x.trangThai;
				nhanVien.AppendChild(trangThai);

				ngayLamThem.AppendChild(nhanVien);
				doc.Save(filePath);
				return true;

			}
			else
			{
				XmlElement nhanVien = doc.CreateElement("NhanVien");
				XmlElement ngayLamThemCreate = doc.CreateElement("NgayLamThem");
				XmlAttribute ngay = doc.CreateAttribute("Ngay");
				ngay.InnerText = x.ngayLamThem;
				ngayLamThemCreate.Attributes.Append(ngay);

				XmlAttribute ma = doc.CreateAttribute("Ma");
				ma.InnerText = x.maNhanVien;
				nhanVien.Attributes.Append(ma);

				XmlElement loaiLamThem = doc.CreateElement("LoaiLamThem");
				loaiLamThem.InnerText = x.loaiLamThem;
				nhanVien.AppendChild(loaiLamThem);

				XmlElement soGio = doc.CreateElement("SoGio");
				soGio.InnerText = x.soGio.ToString();
				nhanVien.AppendChild(soGio);

				XmlElement trangThai = doc.CreateElement("TrangThai");
				trangThai.InnerText = x.trangThai;
				nhanVien.AppendChild(trangThai);

				ngayLamThemCreate.AppendChild(nhanVien);
				root.AppendChild(ngayLamThemCreate);
				doc.Save(filePath);
				
				
				return true;
			}
			
		}

		public bool xoa(NhanVien nhanVien)
		{

			XmlNode nv = doc.SelectSingleNode("/DangKyLamThem/NgayLamThem[@Ngay='" + nhanVien.ngayLamThem + "']");
		
			XmlNode maNv = nv.SelectSingleNode("NhanVien[@Ma = '" + nhanVien.maNhanVien + "']");

			if (maNv != null)
			{
				DialogResult rs = MessageBox.Show("Bạn có muốn chắc chắn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
				if (rs == DialogResult.Yes) { 
					nv.RemoveChild(maNv);
					doc.Save(filePath);
					return true;
				}
			}
			return false;
		}

		public bool sua(NhanVien nhanVien)
		{

			XmlNode nv = doc.SelectSingleNode("/DangKyLamThem/NgayLamThem[@Ngay='" + nhanVien.ngayLamThem + "']");

			//XmlNode maNv = nv.SelectSingleNode("NhanVien[@Ma = '" + nhanVien.maNhanVien + "']");
			XmlNode maNv = nv.SelectSingleNode("NhanVien");

			if (maNv != null)
			{
				maNv.Attributes["Ma"].InnerText = nhanVien.maNhanVien;

				maNv.SelectSingleNode("LoaiLamThem").InnerText = nhanVien.loaiLamThem;
				maNv.SelectSingleNode("SoGio").InnerText = nhanVien.soGio.ToString();
				maNv.SelectSingleNode("TrangThai").InnerText = nhanVien.trangThai;

				//if (kiemTraNhanVien(nhanVien))
				//{
				//	MessageBox.Show("Thông tin đã tồn tại");
				//	return true;
				//}

				doc.Save(filePath);
				return true;
			}
			return false;
		}
		
		 public void tim(string ngay, DataGridView dgv)
        {
            XmlNode nlv = doc.SelectSingleNode("/DangKyLamThem/NgayLamThem[@Ngay = '" + ngay + "']");
            if(nlv != null)
            {
                int index = 0;
                XmlNodeList nhanViens = nlv.SelectNodes("NhanVien");
                dgv.Rows.Clear();
                foreach (XmlNode nv in nhanViens)
                {
                    dgv.Rows.Add();
                    dgv.Rows[index].Cells[0].Value = nlv.SelectSingleNode("@Ngay").InnerText;
                    dgv.Rows[index].Cells[1].Value = nv.SelectSingleNode("@Ma").InnerText;
                    dgv.Rows[index].Cells[2].Value = nv.SelectSingleNode("LoaiLamThem").InnerText;
                    dgv.Rows[index].Cells[3].Value = nv.SelectSingleNode("SoGio").InnerText;
                    dgv.Rows[index].Cells[4].Value = nv.SelectSingleNode("TrangThai").InnerText;
                    index++;
                }
                return;
            }
            MessageBox.Show("Ngay lam viec khong ton tai a!");
        }


	}

}
