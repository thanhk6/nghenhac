using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using VSW.Lib.Models;
using System.IO;
//using Excel=Microsoft.Office.Interop.Excel;
//using Microsoft.Office.Interop.Excel;
using System.Drawing;
using Aspose.Cells;
namespace VSW.Lib.Global
{
    public class Excel
    {
        public static void Export(List<List<object>> list, int start_row, string sourceFile, string exportFile)
        {
            if (list == null) return;

            Workbook workbook = new Workbook();
            workbook.Open(sourceFile);
            Cells cells = workbook.Worksheets[0].Cells;

            for (int i = 0; i < list.Count; i++)
            {
                for (int j = 0; j < list[i].Count; j++)
                {
                    cells[start_row + i, j].PutValue(list[i][j]);
                }

            }

            workbook.Save(exportFile);
        }
        //public static void Export(List<List<object>> list, int start_row, string sourceFile, string exportFile)
        //{
        //    string html;
        //    try
        //    {
        //        string saveExcelFile = exportFile;

        //        Excel.Application xlApp = new Excel.Application();

        //        if (xlApp == null)
        //        {
        //            html = "lỗi không sử dụng được excel";
        //            return;
        //        }
        //        xlApp.Visible = false;

        //        object misValue = System.Reflection.Missing.Value;
        //        Excel.Workbook wb = xlApp.Workbooks.Add(misValue);

        //        Excel.Worksheet ws = (Excel.Worksheet)wb.Worksheets[1];

        //        if (ws == null)
        //        {
        //            html = "Không thể tạo được WorkSheet";
        //            return;
        //        }
        //        int row = 1;
        //        string fontName = "Times New Roman";
        //        int fontSizeTieuDe = 18;
        //        int fontSizeTenTruong = 14;
        //        int fontSizeNoiDung = 12;
        //        //Xuất dòng Tiêu đề của File báo cáo: Lưu ý

        //        Excel.Range row1_TieuDe_ThongKeSanPham = ws.get_Range("A1", "E1");
        //        row1_TieuDe_ThongKeSanPham.Merge();
        //        row1_TieuDe_ThongKeSanPham.Font.Size = fontSizeTieuDe;
        //        row1_TieuDe_ThongKeSanPham.Font.Name = fontName;
        //        row1_TieuDe_ThongKeSanPham.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
        //        row1_TieuDe_ThongKeSanPham.Value2 = "Thống kê sản phẩm";

        //        //Tạo Ô Số Thứ Tự (STT)
        //        Excel.Range row23_STT = ws.get_Range("A2", "A3");//Cột A dòng 2 và dòng 3
        //        row23_STT.Merge();
        //        row23_STT.Font.Size = fontSizeTenTruong;
        //        row23_STT.Font.Name = fontName;
        //        row23_STT.Cells.HorizontalAlignment = XlHAlign.xlHAlignCenter;
        //        row23_STT.Value2 = "STT";

        //        //Tạo Ô Mã Sản phẩm :
        //        Excel.Range row23_MaSP = ws.get_Range("B2", "B3");//Cột B dòng 2 và dòng 3
        //        row23_MaSP.Merge();
        //        row23_MaSP.Font.Size = fontSizeTenTruong;
        //        row23_MaSP.Font.Name = fontName;
        //        row23_MaSP.Cells.HorizontalAlignment = XlHAlign.xlHAlignCenter;
        //        row23_MaSP.Value2 = "Mã Sản Phẩm";
        //        row23_MaSP.ColumnWidth = 20;

        //        //Tạo Ô Tên Sản phẩm :
        //        Excel.Range row23_TenSP = ws.get_Range("C2", "C3");//Cột C dòng 2 và dòng 3
        //        row23_TenSP.Merge();
        //        row23_TenSP.Font.Size = fontSizeTenTruong;
        //        row23_TenSP.Font.Name = fontName;
        //        row23_TenSP.Cells.HorizontalAlignment = XlHAlign.xlHAlignCenter;
        //        row23_TenSP.ColumnWidth = 20;
        //        row23_TenSP.Value2 = "Tên Sản Phẩm";

        //        //Tạo Ô Giá Sản phẩm :
        //        Excel.Range row2_GiaSP = ws.get_Range("D2", "E2");//Cột D->E của dòng 2
        //        row2_GiaSP.Merge();
        //        row2_GiaSP.Font.Size = fontSizeTenTruong;
        //        row2_GiaSP.Font.Name = fontName;
        //        row2_GiaSP.Cells.HorizontalAlignment = XlHAlign.xlHAlignCenter;
        //        row2_GiaSP.Value2 = "Giá Sản Phẩm";

        //        //Tạo Ô Giá Nhập:
        //        Excel.Range row3_GiaNhap = ws.get_Range("D3", "D3");//Ô D3
        //        row3_GiaNhap.Font.Size = fontSizeTenTruong;
        //        row3_GiaNhap.Font.Name = fontName;
        //        row3_GiaNhap.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
        //        row3_GiaNhap.Value2 = "Giá Nhập";
        //        row3_GiaNhap.ColumnWidth = 20;

        //        //Tạo Ô Giá Xuất:
        //        Excel.Range row3_GiaXuat = ws.get_Range("E3", "E3");//Ô E3
        //        row3_GiaXuat.Font.Size = fontSizeTenTruong;
        //        row3_GiaXuat.Font.Name = fontName;
        //        row3_GiaXuat.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
        //        row3_GiaXuat.Value2 = "Giá Xuất";
        //        row3_GiaXuat.ColumnWidth = 20;
        //        //Tô nền vàng các cột tiêu đề:
        //        Excel.Range row23_CotTieuDe = ws.get_Range("A2", "E3");
        //        //nền vàng
        //        row23_CotTieuDe.Interior.Color = ColorTranslator.ToOle(System.Drawing.Color.Yellow);
        //        //in đậm
        //        row23_CotTieuDe.Font.Bold = true;
        //        //chữ đen
        //        row23_CotTieuDe.Font.Color = ColorTranslator.ToOle(System.Drawing.Color.Black);

        //        int stt = 0;
        //        row = 3;
        //        //dữ liệu xuất bắt đầu từ dòng số 4 trong file Excel (khai báo 3 để vào vòng lặp nó ++ thành 4)
        //        //CSDL_MAUDataContext context = new CSDL_MAUDataContext();
        //        for (int i = 0; list != null && i < list.Count; i++)
        //        {
        //            stt++;
        //            row++;

        //            dynamic[] arr = { 1, 2 };

        //            Excel.Range rowData = ws.get_Range("A" + row, "E" + row);//Lấy dòng thứ row ra để đổ dữ liệu
        //            rowData.Font.Size = fontSizeNoiDung;
        //            rowData.Font.Name = fontName;
        //            rowData.Value2 = arr;
        //        }


        //        //Kẻ khung toàn bộ
        //        //BorderAround(ws.get_Range("A2", "E" + row));

        //        //Lưu file excel xuống Ổ cứng
        //        wb.SaveAs(saveExcelFile);

        //        //đóng file để hoàn tất quá trình lưu trữ
        //        wb.Close(true, misValue, misValue);
        //        //thoát và thu hồi bộ nhớ cho COM
        //        xlApp.Quit();
        //        releaseObject(ws);
        //        releaseObject(wb);
        //        releaseObject(xlApp);

        //        //Mở File excel sau khi Xuất thành công
        //        System.Diagnostics.Process.Start(saveExcelFile);
        //    }
        //    catch (Exception ex)
        //    {
        //        html = (ex.Message);
        //    }
        //}
        ////Hàm kẻ khung cho Excel
        //private void BorderAround(Excel.Range range)
        //{
        //    Excel.Borders borders = range.Borders;
        //    borders[XlBordersIndex.xlEdgeLeft].LineStyle = XlLineStyle.xlContinuous;
        //    borders[XlBordersIndex.xlEdgeTop].LineStyle = XlLineStyle.xlContinuous;
        //    borders[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
        //    borders[XlBordersIndex.xlEdgeRight].LineStyle = XlLineStyle.xlContinuous;
        //    borders.Color = Color.Black;
        //    borders[XlBordersIndex.xlInsideVertical].LineStyle = XlLineStyle.xlContinuous;
        //    borders[XlBordersIndex.xlInsideHorizontal].LineStyle = XlLineStyle.xlContinuous;
        //    borders[XlBordersIndex.xlDiagonalUp].LineStyle = XlLineStyle.xlLineStyleNone;
        //    borders[XlBordersIndex.xlDiagonalDown].LineStyle = XlLineStyle.xlLineStyleNone;
        //}
        ////Hàm thu hồi bộ nhớ cho COM Excel
        //private static void releaseObject(object obj)
        //{
        //    try
        //    {
        //        System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
        //        obj = null;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        obj = null;
        //    }
        //    finally
        //    { GC.Collect(); }
        //}
        public static int Import(string file, int menuID)
        {
            if (!file.EndsWith(".xls") && !file.EndsWith(".xlsx")) return 0;

            var fstream = new FileStream(file, FileMode.Open);
            var workbook = new Workbook();
            workbook.Open(fstream);
            var worksheet = workbook.Worksheets[0];

            if (worksheet == null || worksheet.Cells.MaxRow < 1) return 0;

            int success = 0;

            for (int i = 1; i <= worksheet.Cells.MaxRow; i++)
            {
                string name = worksheet.Cells[i, 0].StringValue.Trim();
                string model = worksheet.Cells[i, 1].StringValue.Trim();
                long price = Core.Global.Convert.ToLong(worksheet.Cells[i, 2].StringValue.Trim().Replace(".", "").Replace(",", ""));
                string summary = worksheet.Cells[i, 3].StringValue.Trim();
                string content = worksheet.Cells[i, 4].StringValue.Trim();

                string code = Data.GetCode(name);

                try
                {
                    var item = new ModProductEntity()
                    {
                        ID = 0,
                        MenuID = menuID,
                        Name = name,
                        Code = code,
                        Model = model,
                        Price = price,
                        Summary = summary,
                        Content = content,
                        Order = GetMaxOrder(menuID),
                        Published = DateTime.Now,
                        Updated = DateTime.Now,
                        Activity = true
                    };

                    ModProductService.Instance.Save(item);

                    //update url;
                    ModCleanURLService.Instance.InsertOrUpdate(item.Code, "Product", item.ID, item.MenuID, item.GetMenu().LangID);

                    success++;
                }
                catch
                {
                    continue;
                }
            }

            fstream.Close();
            fstream.Dispose();

            return success;
        }



        public static void ImportCity(string file, int langID)
        {
            if (!file.EndsWith(".xls") && !file.EndsWith(".xlsx")) return;

            FileStream fstream = new FileStream(file, FileMode.Open);
            Workbook workbook = new Workbook();
            workbook.Open(fstream);
            Worksheet worksheet = workbook.Worksheets[0];

            if (worksheet == null || worksheet.Cells.MaxRow < 1) return;

            for (int i = 1; i <= worksheet.Cells.MaxRow; i++)
            {
                string _Name = worksheet.Cells[i, 0].StringValue.Trim();
                string _Code = Data.GetCode(_Name);

                var _item = WebMenuService.Instance.CreateQuery()
                                                .Where(o => o.Activity == true && o.Type == "City" && o.Code == _Code)
                                                .ToSingle_Cache();
                if (_item == null)
                {
                    _item = new WebMenuEntity();

                    _item.Name = _Name;
                    _item.Code = _Code;
                    _item.Type = "City";
                    _item.CityCode = worksheet.Cells[i, 1].StringValue.Trim();

                    _item.ParentID = 8;
                    _item.LangID = langID;

                    _item.Order = GetMaxOrder(langID, 8);
                    _item.Activity = true;

                    WebMenuService.Instance.Save(_item);
                }
            }

            fstream.Close();
            fstream.Dispose();
        }

        public static void ImportDistrict(string file, int langID)
        {
            if (!file.EndsWith(".xls") && !file.EndsWith(".xlsx")) return;

            FileStream fstream = new FileStream(file, FileMode.Open);
            Workbook workbook = new Workbook();
            workbook.Open(fstream);
            Worksheet worksheet = workbook.Worksheets[0];

            if (worksheet == null || worksheet.Cells.MaxRow < 1) return;

            for (int i = 1; i <= worksheet.Cells.MaxRow; i++)
            {
                string _Name = worksheet.Cells[i, 2].StringValue.Trim();
                string _Code = Data.GetCode(_Name);
                string _CityCode = worksheet.Cells[i, 1].StringValue.Trim();

                var _item = WebMenuService.Instance.CreateQuery()
                                    .Where(o => o.Activity == true && o.Type == "City" && o.Code == _Code)
                                    .ToSingle_Cache();

                var parent = WebMenuService.Instance.CreateQuery()
                                    .Where(o => o.Activity == true && o.Type == "City" && o.CityCode == _CityCode)
                                    .ToSingle_Cache();

                if (_item == null)
                {
                    _item = new WebMenuEntity();

                    _item.Name = _Name;
                    _item.Code = _Code;
                    _item.Type = "City";

                    _item.ParentID = parent != null ? parent.ID : 5;
                    _item.LangID = langID;

                    _item.Order = GetMaxOrder(langID, _item.ParentID);
                    _item.Activity = true;

                    WebMenuService.Instance.Save(_item);
                }
            }

            fstream.Close();
            fstream.Dispose();
        }

        private static int GetMaxOrder(int langID, int parentID)
        {
            return WebMenuService.Instance.CreateQuery()
                            .Where(o => o.LangID == langID && o.ParentID == parentID)
                            .Max(o => o.Order)
                            .ToValue().ToInt(0) + 1;
        }

        private static int GetMaxOrder(int menuID)
        {
            return ModProductService.Instance.CreateQuery()
                            .Where(o => o.MenuID == menuID)
                            .Max(o => o.Order)
                            .ToValue().ToInt(0) + 1;
        }
    }
}

