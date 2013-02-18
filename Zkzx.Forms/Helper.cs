using System;
using System.Collections.Generic;
using System.Text;
using Feng;
using Feng.Grid;
using Zkzx.Model;

namespace Zkzx.Forms
{
    public static class Helper
    {
        public static string DateTime2String(DateTime? time)
        {
            return time.HasValue ? time.Value.ToString("MM.dd-HH:mm") : "__.__-__:__";
        }
        public static string DateTime2String(DateTime? time1, DateTime? time2)
        {
            return DateTime2String(time1) + " - " + DateTime2String(time2);
        }

        public static void SetCellButton(IBoundGrid grid, string columnName, System.Windows.Forms.Button btn)
        {
            Xceed.Grid.CellRow row = null;
            foreach (Xceed.Grid.Row i in grid.FixedFooterRows)
            {
                row = i as Xceed.Grid.CellRow;
                if (row != null && row.Cells[columnName] != null)
                {
                    break;
                }
            }
            if (row != null)
            {
                btn.Parent.Controls.Remove(btn);
                btn.Visible = true;

                grid.DisplayManager.SearchManager.DataLoaded +=new EventHandler<DataLoadedEventArgs>( (sender, e) =>
                {
                    Xceed.Grid.CellRow row2 = null;
                    foreach (Xceed.Grid.Row i in grid.FixedFooterRows)
                    {
                        row2 = i as Xceed.Grid.CellRow;
                        if (row2 != null && row2.Cells[columnName] != null)
                        {
                            break;
                        }
                    }
                    if (row2 != null)
                    {
                        row2.Cells[columnName].CellViewerManager = new Xceed.Grid.Viewers.CellViewerManager(btn, string.Empty);
                        row2.Cells[columnName].CellEditorManager = new Xceed.Grid.Editors.CellEditorManager(btn, string.Empty, true, true);
                    }
                });
                row.Cells[columnName].CellViewerManager = new Xceed.Grid.Viewers.CellViewerManager(btn, string.Empty);
                row.Cells[columnName].CellEditorManager = new Xceed.Grid.Editors.CellEditorManager(btn, string.Empty, true, true);
            }
        }

        public static void SetGridDefault(Feng.Windows.Forms.MyTemplateForm mainForm, Feng.Grid.DataUnboundGrid grid)
        {
            grid.GetColumnManageRow().AllowColumnReorder = false;
            grid.SelectionMode = System.Windows.Forms.SelectionMode.One;
            grid.SelectionBackColor = System.Drawing.Color.LightSkyBlue;
            grid.GetColumnManageRow().ShowFixedColumnSplitter = Xceed.Grid.ShowFixedColumnSplitter.WhenFixedColumnsExist;

            grid.DisplayManager.SearchManager.EnablePage = false;
            mainForm.EnableInvalidateAfterSearch(grid.DisplayManager.SearchManager, grid);

            mainForm.EnableSearchProgressForm(grid.DisplayManager.SearchManager);

            grid.SingleClickEdit = true;
        }

        public static void btn批量确认_Click(Xceed.Grid.GridControl grid, Action<object, System.EventArgs> action, string selectColumnName = "选定", string confirmColumnName = "确认")
        {
            foreach (Xceed.Grid.DataRow row in grid.DataRows)
            {
                bool? b = Feng.Utils.ConvertHelper.ToBoolean(row.Cells[selectColumnName].Value);
                if (b.HasValue && b.Value)
                {
                    action(row.Cells[confirmColumnName], System.EventArgs.Empty);
                }
            }
        }
    }
}
