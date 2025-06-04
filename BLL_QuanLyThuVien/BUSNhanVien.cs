using DAL_QuanLyThuVien;
using DTO_QuanLyThuVien;
using System;
using System.Collections.Generic;

namespace BLL_QuanLyThuVien
{
    public class BUSNhanVien
    {
        private readonly DALNhanVien dalNhanVien = new DALNhanVien();

        public NhanVien DangNhap(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                return null;

            return dalNhanVien.getNhanVien1(username, password);
        }

        public bool ResetMatKhau(string email, string mk)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(mk))
                return false;

            try
            {
                dalNhanVien.ResetMatKhau(mk, email);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<NhanVien> GetNhanViens()
        {
            return dalNhanVien.selectAll();
        }

        public string InsertNhanVien(NhanVien nv)
        {
            if (nv == null)
                return "Nhân viên không hợp lệ.";

            try
            {
                nv.MaNhanVien = dalNhanVien.taomanhanvien();

                if (string.IsNullOrEmpty(nv.MaNhanVien))
                    return "Mã nhân viên không hợp lệ.";

                if (dalNhanVien.checkemailexits(nv.Email))
                    return "Email đã tồn tại.";

                dalNhanVien.insert(nv);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "Lỗi khi thêm nhân viên: " + ex.Message;
            }
        }

        public string XoaNhanVien(string maNV)
        {
            if (string.IsNullOrWhiteSpace(maNV))
                return "Mã nhân viên không hợp lệ.";

            try
            {
                dalNhanVien.deleteNhanVien(maNV);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "Lỗi: " + ex.Message;
            }
        }

        public string UpdateNhanVien(NhanVien nv)
        {
            if (nv == null || string.IsNullOrEmpty(nv.MaNhanVien))
                return "Mã nhân viên không hợp lệ.";

            try
            {
                dalNhanVien.updateNhanVien(nv);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "Lỗi: " + ex.Message;
            }
        }
    }
}
