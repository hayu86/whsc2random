   M S B A M L   `   `   `  ���� F  Bwhsc2random, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:whsc2randomwhsc2random  ?$clr-namespace:whsc2random.Propertieswhsc2random.Properties      whsc2random.MainWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     +localclr-namespace:whsc2random     ;
Properties$clr-namespace:whsc2random.Properties     -     �Name$  mainWin�� � Title$' !文化市场双随机抽查系统��$��528.595��$��785.472�� � Icon$ logo.png�� � 
ResizeMode$ NoResize=�.��� $��-1,0,-23,-19q� � ColumnDefinitions ��  � RowDefinitions �� .+��� -    ��Name$ btnGetPerson��$��抽取人员��$
��Left=�$��24��$��
21,315,0,0q�$	��Top=�$	��100���� -    ��Name$ lbPerson��$
��Left=�$��	21,43,0,0q�$	��100��$	��Top=�$	��227���� -   $ 
btnGetCity��$��抽取区域��$
��Left=�$��24��$��132,315,0,0q�$	��Top=�$	��100�� J� RenderTransformOrigin$ 0.705,0.539&��� -   $ lbCity��$
��Left=�$��
132,43,0,0q�$	��100��$	��Top=�$	��202���� -   $ btnGetCompany��$��抽取检查对象��$��24��$��243,315,0,0q�$	��Top=�$
��Left=�$	��223���� -   $ 	lbCompany��$��
243,43,0,0q�$
��Left=�$	��Top=�$	��227��$	��223���� -   	 ��Name$	 rtxDataShow��$��481,101,0,0q�$
��Left=�$	��274��$	��347��$	��Top=�
 �� 
IsReadOnly
 . " �� VerticalScrollBarVisibility$
 Auto=�. �/� �� -	   $ lbPersonSelected��$
��Left=�$��
21,352,0,0q�$	��100��$��96��$	��Top=��� -
   $ lbCitySelected��$
��Left=�$��132,352,0,0q�$	��100��$��96��$	��Top=��� -   $ lbCompanySelected��$��243,352,0,0q�$��96��$	��223��$	��Top=�$
��Left=��� -    ��Name$ label��$��抽取数量��$��
15,280,0,0q�$
��Left=�$��24��$	��Top=�$��61���� -   $ label1��$��抽取数量��$��127,280,0,0q�$
��Left=�$��24��$	��Top=�$��60���� -    ��Name$ txtCountCity��$��2��$��40��$��192,280,0,0q�$
��Left=�$��Center=�$��24��$	��Top=��� -   $ txtCountPerson��$��2��$��40��$��
81,280,0,0q�$
��Left=�$��Center=�$��24��$	��Top=��� -   $ label3��$��抽取数量��$��367,280,0,0q�$��24��$	��Top=�$
��Left=�$��57���� -   $ txtCountCompany��$��2��$��426,280,0,0q�$��24��$	��Top=�$��Center=�$
��Left=�$��40��$��0q� �� TextDecorations Z� �� -   $ label3_Copy��$��市场��$��238,280,0,0q�$��24��$	��Top=�$
��Left=��� -    ��Name$ cmbTypeOfCompany��$��272,282,0,0q�$	��Top=�$
��Left=�$��90���� -   $ txtMakerName��$
��Left=�$	��203��$��
552,43,0,0q�$��24��$��Center=�$	��Top=�$ 0.612,0.355&� �� MaxLines$ 1���� -   $ label3_Copy1��$��制作单位��$��
475,43,0,0q�$��24��$	��Top=�$
��Left=�$��77���� -   $ label3_Copy2��$��数据��$��
475,72,0,0q�$��24��$	��Top=�$
��Left=�$	��210���� -   $ btnSave��$��保存已抽取��$
��Left=�$��24��$��132,457,0,0q�$	��Top=�$	��334���� -   $ btnGetOneAndSave��$��抽取一组并保存��$
��Left=�$��24��$��118,316,0,0q�$	��Top=�$	��150��$ 0.705,0.539&�{�.  $y�Hidden=��� -   $ btnGetAllAndSave��$��抽取全部并保存��$��24��$��275,316,0,0q�$	��Top=�$
��Left=�$	��150��{�.  $y�Hidden=��� -   $ btnPrintPreview��$��打印预览��$
��Left=�$��24��$��621,457,0,0q�$	��Top=�$��64���� -   $ btnReset��$��重置��$
��Left=�$��24��$��
21,457,0,0q�$	��Top=�$��44���� -   $ label3_Copy3��$��执法人员��$��	17,15,0,0q�$��24��$	��Top=�$
��Left=�$	��103���� -   $ label3_Copy4��$��区域��$��
125,15,0,0q�$��24��$	��Top=�$
��Left=�$	��107���� -   $ label3_Copy5��$��经营单位��$��
240,15,0,0q�$��24��$	��Top=�$
��Left=�$	��161���� -   $ label3_Copy6��$��已抽取数据��$��
475,15,0,0q�$��24��$	��Top=�$
��Left=�$	��280���� -     ��Name$ checkBox��$��可重复抽取��$
��Left=�$��132,255,0,0q�$	��Top=�$ 0.915,-0.809&��� -!   $ btnPrint��$��打印��$
��Left=�$��24��$��691,457,0,0q�$	��Top=�$��64���� -"   $ 
btnPageSet��$��页面设置��$
��Left=�$��24��$��482,457,0,0q�$	��Top=�$��64���� -#   $ btnPrintSet��$��打印设置��$
��Left=�$��24��$��552,457,0,0q�$	��Top=�$��64��