   M S B A M L   `   `   `  ����L  HDatabaseInterface, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:DatabaseInterfaceDatabaseInterface  5   	   K*clr-namespace:DatabaseInterface.ViewModelsDatabaseInterface.ViewModels  5   	   "    DatabaseInterface.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:DatabaseInterface     5   	   9vm*clr-namespace:DatabaseInterface.ViewModels     5   	   -   5          �Name$  connectButton��5   	    � Title$ Database Management Tool��5	   	   $	��450��6*   $	��800��67    � DataContext 5
      0  *DatabaseInterface.ViewModels.MainViewModel  5   
   .��5      � .+�5   
   �� $��Connect��6   
�������$
��Left=�6W   $��	10,10,0,0q�6r   $	��Top=�6�   $	��150��6�   $��24��6�   ��66   �� *MssqlCommand+6�   �� 5   
   -   5         ��Name$ dbStatusTextBlock��5      $
��Left=�6w   $��
112,39,0,0q�6�   $
��Wrap=�6�   $	��Top=�6�    J� RenderTransformOrigin$ 0.291,-0.237&�6�   $��48��6�   ��60   �� *DbModel.dbStatus+ �� UpdateSourceTrigger$ PropertyChanged=���5      �� 5       �� 
TargetType" ��6    �� Triggers 5      �� 5       �� Binding 6&   �� *DbModel.color+	 �� Value	 true6H   .a�5      �� 
 �� Property
 � �� �� Value
 ����6;   5      5      5      �� 5   
   -   5         ��Name$ dbStatusLabel��5      $��Database status:��6(   $
��Left=�6C   $��	10,34,0,0q�6^   $	��Top=�6q   $	��132��6�   �� 5   
   -   5         ��Name$ dbTablelIstBox��5      $
��Left=�6~   $	��331��6�   $��	10,65,0,0q�6�   $	��Top=�6�   $	��150��6�   ��6*   �� *DbModel.dbTables+ �� SelectedItem 6S   �� *SelectedTableName+6�   &   System.Windows.Controls.DataGrid  5!   
   -   5          Name$ dataGridableData��5!      $
��Left=�6-      AutoGenerateColumns . $	��331��6�   $��
174,65,0,0q�6�   $	��Top=�6�   $	��598��6�   ��6H   �� *	TableData+6�   �� 5#   
   -   5         ��Name$ exportToCSVBut��5#      $��Export to CSV��6q   
�������$
��Left=�6�   $��
174,10,0,0q�6�   $	��Top=�6�   $	��155��6�   $��24��6�   {�6)   �� *ExportToCsvStatus+��6R   �� *
CsvCommand+6�   �� 5%   
   -   5         ��Name$ SearchTextBox��5%      $
��Left=�6R   $��24��6m   $��
343,10,0,0q�6y    �� TextWrapping$
 Wrap=�6�   $	��Top=�6   $	��429��6  {�6)   �� *ExportToCsvStatus+��6�   �� *SearchTable+$ PropertyChanged=� �� ValidatesOnDataErrors . 6  5&      5'      