# How to navigate to next row with Enter on Android in .NET Maui DataGrid SfDataGrid?
This sample demonstrates how to navigate to next row with Enter on Android in [.NET Maui DataGrid](https://help.syncfusion.com/maui/datagrid/overview)? (SfDataGrid).
It achieves this by customizing the text cell renderer to handle the Enter key and programmatically move the current cell to the next row during editing.

## Xaml
```
    <ContentPage.BindingContext>
        <local:EmployeeViewModel x:Name="viewModel" />
    </ContentPage.BindingContext>

    <syncfusion:SfDataGrid x:Name="sfGrid"
                       SelectionMode="Single"
                       NavigationMode="Cell"
                       AllowEditing="True"
                       GridLinesVisibility="Both"
                       HeaderGridLinesVisibility="Both"
                       ColumnWidthMode="Auto"
                       AutoGenerateColumnsMode="None"
                       ItemsSource="{Binding Employees}">

        <syncfusion:SfDataGrid.Columns>
            <syncfusion:DataGridNumericColumn MappingName="EmployeeID"
                                          Format="#"
                                          HeaderText="Employee ID" />
            <syncfusion:DataGridTextColumn MappingName="Name"
                                       HeaderText="Employee Name" />
            <syncfusion:DataGridTextColumn MappingName="Title"
                                       HeaderText="Designation" />
            <syncfusion:DataGridTextColumn HeaderText="Gender"
                                       MappingName="Gender" />
        </syncfusion:SfDataGrid.Columns>
    </syncfusion:SfDataGrid>

```
## Xaml.cs
```
 public partial class MainPage : ContentPage
 {
     public MainPage()
     {
         InitializeComponent();
         sfGrid.CellRenderers.Remove("Text");
         sfGrid.CellRenderers.Add("Text", new CustomTextRenderer());
     }
 }
```
## CustomTextRenderer.cs
```
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

```

### ScreenShot
<img src="https://support.syncfusion.com/kb/agent/attachment/article/22728/inline?token=eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjU5MzIzIiwib3JnaWQiOiIzIiwiaXNzIjoic3VwcG9ydC5zeW5jZnVzaW9uLmNvbSJ9.V5X0GIj2RNy7XmFOOohYK9PONahZQticvUaZ_7G_aVs" width=800/>

[View sample in GitHub](https://github.com/SyncfusionExamples/How-to-navigate-to-next-row-with-Enter-on-Android-in-.NET-Maui-DataGrid-SfDataGrid)

 Take a moment to explore this [documentation](https://help.syncfusion.com/maui/datagrid/overview), where you can find more information about Syncfusion .NET MAUI DataGrid (SfDataGrid) with code examples. Please refer to this [link](https://www.syncfusion.com/maui-controls/maui-datagrid) to learn about the essential features of Syncfusion .NET MAUI DataGrid (SfDataGrid).

### Conclusion
I hope you enjoyed learning about How to bind a dynamic data object in SfDataGrid.

You can refer to our [.NET MAUI DataGrid’s feature tour](https://www.syncfusion.com/maui-controls/maui-datagrid) page to learn about its other groundbreaking feature representations. You can also explore our [.NET MAUI DataGrid Documentation](https://help.syncfusion.com/maui/datagrid/getting-started) to understand how to present and manipulate data. For current customers, you can check out our .NET MAUI components on the [License and Downloads](https://www.syncfusion.com/sales/teamlicense) page. If you are new to Syncfusion, you can try our 30-day [free trial](https://www.syncfusion.com/downloads/maui) to explore our .NET MAUI DataGrid and other .NET MAUI components.

If you have any queries or require clarifications, please let us know in the comments below. You can also contact us through our [support forums](https://www.syncfusion.com/forums),[Direct-Trac](https://support.syncfusion.com/create) or [feedback portal](https://www.syncfusion.com/feedback/maui?control=sfdatagrid), or the feedback portal. We are always happy to assist you!
