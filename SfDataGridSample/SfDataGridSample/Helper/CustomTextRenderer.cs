using Syncfusion.Maui.DataGrid;
using Syncfusion.Maui.GridCommon.ScrollAxis;
using Syncfusion.Maui.Inputs;
using System;
using System.Collections.Generic;
using System.Text;

namespace SfDataGridSample.Helper
{
    public class CustomTextRenderer : DataGridTextBoxCellRenderer
    {
        public override void OnInitializeEditView(DataColumnBase dataColumn, SfDataGridEntry view)
        {
            base.OnInitializeEditView(dataColumn, view);
            if (view != null && view is SfDataGridEntry entry)
            {
                entry.HorizontalTextAlignment = TextAlignment.Center;
                entry.VerticalTextAlignment = TextAlignment.Center;
                entry.ReturnType = ReturnType.Next;
                entry.ReturnCommand = new Command(() => OnEntryCompleted(entry, EventArgs.Empty));
            }
        }

        private void OnEntryCompleted(object? sender, EventArgs e)
        {
            if (sender is Entry entry && entry.ReturnType == ReturnType.Next)
            {
                DataGrid!.EndEdit();

                var current = DataGrid.CurrentCellManager.RowColumnIndex;
                var nextRowIndex = current.RowIndex + 1;
                var colIndex = current.ColumnIndex;

                if (nextRowIndex >= DataGrid.GetVisualContainer().RowCount)
                    return;

                var next = new RowColumnIndex(nextRowIndex, colIndex);
                DataGrid.MoveCurrentCellTo(next);
                DataGrid.ScrollToRowColumnIndex(
                    next.RowIndex,
                    next.ColumnIndex,
                    ScrollToPosition.MakeVisible,
                    ScrollToPosition.MakeVisible,
                    false
                );

                DataGrid.Dispatcher.Dispatch(() =>
                {
                    DataGrid.BeginEdit(next.RowIndex, next.ColumnIndex);
                });
            }
        }

        public override void CommitCellValue(bool isNewValue)
        {
            base.CommitCellValue(isNewValue);
        }

        public override bool EndEdit(DataColumnBase dataColumn, object record, bool canResetBinding = false)
        {
            return base.EndEdit(dataColumn, record, canResetBinding);
        }
    }


}
