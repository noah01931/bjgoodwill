using System;
using System.Collections.Generic;
using System.Text;
//using Microsoft.Office.Tools.Word;
using Word=Microsoft.Office.Interop.Word;
using Microsoft.Office.Core;
using System.IO;
using Microsoft.Office.Interop.Word;

namespace ClassGenerate.Proc
{
    public enum Align { 
        /// <summary>
        /// 水平垂直居中
        /// </summary>
        HVCenter, HorizontalLeft, HorizontalCenter, HorizontalRight, VerticalTop, VerticalCenter, VerticalBottom};
    public class ProcWord
    {
        /// <summary>
        /// application实例
        /// </summary>
        Microsoft.Office.Interop.Word.Application app;
        /// <summary>
        /// word空引用
        /// </summary>
        object Missing = System.Reflection.Missing.Value;
        /// <summary>
        /// word文档
        /// </summary>
        Microsoft.Office.Interop.Word.Document document;

        public object FileName = @"c:\myfile.doc";

        public object Visible = false;
        Table table = null;
        public ProcWord()
        {
            app = new Word.Application();      
            //app.Visible = true;
            //app.DefaultSaveFormat = "";  //Word 文档 "" 

            object template = Missing;
            object newTemplate = Missing;
            object documentType = Missing;
            document = app.Documents.Add(ref template, ref newTemplate,
                ref documentType, ref Visible);
            document.Activate();
            
            //app.ShowStartupDialog = false; 
        }
        /// <summary>
        /// 添加页眉
        /// </summary>
        public void AddHeader(string headerText, Align align)
        {
            app.ActiveWindow.View.Type = WdViewType.wdOutlineView;
            app.ActiveWindow.View.SeekView = WdSeekView.wdSeekPrimaryHeader;
            app.ActiveWindow.ActivePane.Selection.InsertAfter(headerText);
            SetAlignment(align);
            app.ActiveWindow.View.SeekView = WdSeekView.wdSeekMainDocument;//跳出页眉设置
        }

        /// <summary>
        /// 添加段落
        /// </summary>
        /// <param name="tt"></param>
        /// <returns></returns>
        public Paragraph AddParagraph(WdParagraphAlignment tt)
        {
            object r = document.Words.Last;
            ((Range)r).ParagraphFormat.Alignment = tt;
            Paragraph tem = document.Paragraphs.Add(ref r);
            tem.Alignment = tt;
            return tem;
        }
        public Paragraph AddParagraph()
        {
            object r = document.Words.Last;
            Paragraph tem = document.Paragraphs.Add(ref r);
            return tem;
        }
        /// <summary>
        /// 设置文档的行间距
        /// </summary>
        /// <param name="spacing"></param>
        public void SetLineSpacing(Single spacing)
        {
            app.Selection.ParagraphFormat.LineSpacing = 15f;//设置文档的行间距
        }
        /// <summary>
        /// 移动焦点并换行
        /// </summary>
        public void MoveDown(int rows)
        {
            object count = rows;
            object WdLine = Word.WdUnits.wdLine;//换一行;
            app.Selection.MoveDown(ref WdLine, ref count, ref Missing);//移动焦点
        }
        /// <summary>
        /// 文档中创建表格
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="columns"></param>
        public Table AddTable(int rows, int columns)
        {
            Range r = document.Words.Last;
            object wdwtb = WdDefaultTableBehavior.wdWord9TableBehavior;
            object wdafb = WdAutoFitBehavior.wdAutoFitFixed;
            table = document.Tables.Add(r, rows, columns, ref wdwtb, ref wdafb);

            //table.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
            //table.Borders.InsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
            return table;
        }
        /// <summary>
        /// 填充表格内容
        /// </summary>
        /// <param name="table"></param>
        public Cell AddTableContent(int row, int col 
            , string strContent,int bold, Word.WdColor color)
        { //填充表格内容
            Cell cell = table.Cell(row, col);
            cell.Range.Text = strContent;
            cell.Range.Bold = bold;
            cell.Range.Font.Color = color;
            return cell;
        }
        public Cell AddTableContent(int row, int col, string strContent)
        { //填充表格内容
            Cell cell = table.Cell(row, col);
            cell.Range.Text = strContent;
            return cell;
        }
        /// <summary>
        /// 合并单元格
        /// </summary>
        /// <param name="table"></param>
        /// <param name="startRow"></param>
        /// <param name="startCol"></param>
        /// <param name="endRow"></param>
        /// <param name="endCol"></param>
        public Cell MergeCells(int startRow, int startCol ,int endRow, int endCol)
        {
            //合并单元格
            table.Cell(startRow, startCol).Merge(table.Cell(endRow, endCol));
            return table.Cell(startRow, startCol);
        }
        /// <summary>
        /// 纵向合并单元格
        /// </summary>
        /// <param name="table"></param>
        /// <param name="startRow">起始行</param>
        /// <param name="startCol">起始列</param>
        /// <param name="rows">合并行数</param>
        public Cell MergeCells(int startRow, int startCol, int rows)
        {        //纵向合并单元格
            Cell cell= table.Cell(startRow, startCol);//选中一行
            cell.Select();
            object moveUnit = Word.WdUnits.wdLine;
            object moveCount = rows;
            object moveExtend = Word.WdMovementType.wdExtend;
            app.Selection.MoveDown(ref moveUnit, ref moveCount, ref moveExtend);
            app.Selection.Cells.Merge();
            return cell;
        }
        /// <summary>
        /// 设置对齐方式
        /// </summary>
        /// <param name="paragraphAlignment">水平对齐方式</param>
        /// <param name="vertivalAlignment">垂直对齐方式</param>
        public void SetAlignment(Align align)
        {
            switch (align)
            {
                case Align.HorizontalLeft:
                    app.Selection.ParagraphFormat.Alignment =  WdParagraphAlignment.wdAlignParagraphLeft;
                    break;
                case Align.HorizontalCenter:
                    app.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    break;
                case Align.HorizontalRight:
                    app.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
                    break;
                case Align.VerticalTop:
                    app.Selection.Cells.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalTop;
                    break;
                case Align.VerticalCenter:
                    app.Selection.Cells.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                    break;
                case Align.VerticalBottom:
                    app.Selection.Cells.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalBottom;
                    break;
                case Align.HVCenter:
                    app.Selection.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    app.Selection.Cells.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                    break;
            }
        }

        #region 写字符串
        public void InsertText(string content)
        {
            app.Selection.InsertBefore(content);
        }
        public void AddText(string text)
        {
            Word.Selection currentSelection = app.Selection;

            // Store the user's current Overtype selection
            bool userOvertype = app.Options.Overtype;

            // Make sure Overtype is turned off.
            if (app.Options.Overtype)
            {
                app.Options.Overtype = false;
            }

            currentSelection.Text+=(text);
            // Test to see if selection is an insertion point.
            if (currentSelection.Type == Word.WdSelectionType.wdSelectionIP)
            {
                //currentSelection.TypeText(text);  //不换行
                //currentSelection.TypeParagraph();  
            }

            // Restore the user's Overtype selection
            app.Options.Overtype = userOvertype;
        }
        public void AddTextWithEnd(string content)
        {
            //document.Content.Text += content;
            
            document.Words.Last.Text += content;
        }
        /// <summary>
        /// 写文本,会换行
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public Paragraph WriteText(string text)
        {
            //Paragraph wpPara = document.Content.Paragraphs.Add(ref Missing);
            Paragraph wpPara = AddParagraph();
            //wpPara.Range.InsertParagraph();
            wpPara.Range.Text = text;
            wpPara.WordWrap = 0;//中西文不换行
            //wpPara.Range.InsertAfter(text);
            return wpPara;
        }
        public Paragraph WriteText(string text, Font font)
        {
            Word.Paragraph wpPara = WriteText(text);
            wpPara.Range.Font = font;
            return wpPara;
        }
        public Paragraph WriteText(string text, int bold, float size, WdColor color)
        {
            Word.Paragraph wpPara = WriteText(text);
            wpPara.Range.Font.Bold = bold;
            wpPara.Range.Font.Size = size;
            wpPara.Range.Font.Color = color;
            return wpPara;
        }

        #endregion

        #region 插入图片
        /// <summary>
        /// 插入图片
        /// </summary>
        /// <param name="imgPath"></param>
        /// <param name="width"></param>
        /// <param name="heigth"></param>
        /// <param name="wrapType">图文排版</param>
        public void AddImagin(string imgPath, float width, float heigth, WdWrapType wrapType)
        {
            object LinkToFile = false;
            object SaveWithDocument = true;
            object Anchor = document.Application.Selection.Range;
            document.Application.ActiveDocument.InlineShapes.AddPicture(imgPath, ref LinkToFile, ref SaveWithDocument, ref Anchor);
            //document.Application.ActiveDocument.InlineShapes[1].Width = 100f;//图片宽度
            //document.Application.ActiveDocument.InlineShapes[1].Height = 100f;//图片高度
            document.Application.ActiveDocument.InlineShapes[1].Width = 100f;//图片宽度
            document.Application.ActiveDocument.InlineShapes[1].Height = 100f;//图片高度
            //将图片设置为四周环绕型
            Word.Shape s = document.Application.ActiveDocument.InlineShapes[1].ConvertToShape();
            s.WrapFormat.Type = wrapType;//Word.WdWrapType.wdWrapSquare;
        } 
        #endregion

        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        public void SaveAs()
        {
            //文件保存
            document.SaveAs(ref FileName, ref Missing, ref Missing, ref Missing, ref Missing, ref Missing, ref Missing, ref Missing, ref Missing, ref Missing, ref Missing, ref Missing, ref Missing, ref Missing, ref Missing, ref Missing);
            document.Close(ref Missing, ref Missing, ref Missing);
            app.Quit(ref Missing, ref Missing, ref Missing);
        } 
        #endregion

        public string WriteWordFileTest()
        {
            string message = "";
            try
            {
                //AddTextWithEnd("这用了换行的方法");
                app.Selection.TypeText("ttttttttttttttttttt");
                InsertText("XXXXXXXXXXXXXXXX");
                AddText("这是开始");
                AddText("这是1111111");//不换行
                AddTextWithEnd("换行了");
                //Paragraph p = WriteText("这是开始");
                //AddText(p.Range, "这是1111111");
                //AddText(p.Range, "这是22222222");
               // AddText("111111111111"); AddText("2222222222222222");
                //AddParagraph();//插入段落
                ////添加页眉
                //app.ActiveWindow.View.Type = WdViewType.wdOutlineView;
                //app.ActiveWindow.View.SeekView = WdSeekView.wdSeekPrimaryHeader;
                //app.ActiveWindow.ActivePane.Selection.InsertAfter("[页眉内容]");
                //app.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;//设置右对齐
                //app.ActiveWindow.View.SeekView = WdSeekView.wdSeekMainDocument;//跳出页眉设置
                AddHeader("页眉内容测试", Align.HorizontalRight);

                //app.Selection.ParagraphFormat.LineSpacing = 15f;//设置文档的行间距
                SetLineSpacing(15f);

                ////移动焦点并换行
                //object count = 14;
                //object WdLine = Word.WdUnits.wdLine;//换一行;
                //app.Selection.MoveDown(ref WdLine, ref count, ref Missing);//移动焦点
                app.Selection.TypeParagraph();//插入段落

                ////文档中创建表格
                //Table table = document.Tables.Add(app.Selection.Range, 12, 3, ref Missing, ref Missing);
                ////设置表格样式
                //table.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleThickThinLargeGap;
                //table.Borders.InsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
                //table.Columns[1].Width = 100f;
                //table.Columns[2].Width = 220f;
                //table.Columns[3].Width = 105f;
                Table table = AddTable(12, 3); 

                table.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
                table.Borders.InsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
                table.Columns[1].Width = 100f;
                table.Columns[2].Width = 220f;
                table.Columns[3].Width = 105f;
                //InsertText(DateTime.Now.ToString());
                ////填充表格内容
                //table.Cell(1, 1).Range.Text = "产品详细信息表";
                //table.Cell(1, 1).Range.Bold = 2;//设置单元格中字体为粗体
                AddTableContent(1, 1, "产品详细 信息表", 2, WdColor.wdColorBlack);
                ////合并单元格
                //table.Cell(1, 1).Merge(table.Cell(1, 3));
                Cell cell = MergeCells( 1, 1, 1, 3);

                cell.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;//垂直居中
                cell.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

                ////填充表格内容
                //table.Cell(2, 1).Range.Text = "产品基本信息";
                //table.Cell(2, 1).Range.Font.Color = Word.WdColor.wdColorDarkBlue;//设置单元格内字体颜色
                AddTableContent( 2, 1, "产品基本信息", 0, WdColor.wdColorBlueGray);
                
                ////合并单元格
                //table.Cell(2, 1).Merge(table.Cell(2, 3));
                //app.Selection.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                cell = MergeCells(2, 1, 2, 3);
                cell.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
               
                ////填充表格内容
                //table.Cell(3, 1).Range.Text = "品牌名称：";
                //table.Cell(3, 2).Range.Text = BrandName;
                AddTableContent(3, 1, "品牌名称：", 0, WdColor.wdColorBlue);
                AddTableContent(3, 2, "XX品牌", 0, WdColor.wdColorBlue);
                ////纵向合并单元格
                //table.Cell(3, 3).Select();//选中一行
                //object moveUnit = Word.WdUnits.wdLine;
                //object moveCount = 5;
                //object moveExtend = Word.WdMovementType.wdExtend;
                //app.Selection.MoveDown(ref moveUnit, ref moveCount, ref moveExtend);
                //app.Selection.Cells.Merge();
                MergeCells( 3, 3, 5);
                
                //插入图片
                //string FileName = Picture;//图片所在路径
                //object LinkToFile = false;
                //object SaveWithDocument = true;
                //object Anchor = document.Application.Selection.Range;
                //document.Application.ActiveDocument.InlineShapes.AddPicture(FileName, ref LinkToFile, ref SaveWithDocument, ref Anchor);
                //document.Application.ActiveDocument.InlineShapes[1].Width = 100f;//图片宽度
                //document.Application.ActiveDocument.InlineShapes[1].Height = 100f;//图片高度
                //将图片设置为四周环绕型
                //Word.Shape s = document.Application.ActiveDocument.InlineShapes[1].ConvertToShape();
                //s.WrapFormat.Type = Word.WdWrapType.wdWrapSquare;
                AddImagin(@"c:\1.jpg", 100f, 100f, WdWrapType.wdWrapSquare);
                
                //table.Cell(12, 1).Range.Text = "产品特殊属性";
                //table.Cell(12, 1).Merge(table.Cell(12, 3));
                AddTableContent(12, 1, "产品特殊属性", 1, WdColor.wdColorBlue);

                ////在表格中增加行
                //document.Content.Tables[1].Rows.Add(ref Missing);
                //table.Rows.Add(ref Missing);
                app.Selection.TypeText("");
                //document.Content.Paragraphs.Last.Format.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
                WriteText("文档创建时间：" + DateTime.Now.ToString());
                //AddText("之前换行了");
                app.Selection.TypeText("yyyyyyyyyyyyyyy");
                SaveAs();
            }
            catch (Exception e)
            {
                message = "文件导出异常！";
                System.Windows.Forms.MessageBox.Show(e.Message);
            }
            return message;
        }
    }
}
