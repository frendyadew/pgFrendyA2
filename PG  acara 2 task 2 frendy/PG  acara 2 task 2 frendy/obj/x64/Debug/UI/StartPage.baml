   M S B A M L   `   `   `  ����T  PPG  acara 2 task 2 frendy, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:PG__acara_2_task_2_frendy.UIPG__acara_2_task_2_frendy.UI  5      E'clr-namespace:PG__acara_2_task_2_frendyPG__acara_2_task_2_frendy  5      a ]ArcGIS.Desktop.Extensions, Version=12.8.0.0, Culture=neutral, PublicKeyToken=8fc3cc631e44ad86hJclr-namespace:ArcGIS.Desktop.Extensions;assembly=ArcGIS.Desktop.ExtensionsArcGIS.Desktop.Extensions 5      ,    &PG__acara_2_task_2_frendy.UI.StartPage   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      9ui*clr-namespace:PG__acara_2_task_2_frendy.UI     5      9local'clr-namespace:PG__acara_2_task_2_frendy     5      a
extensionsJclr-namespace:ArcGIS.Desktop.Extensions;assembly=ArcGIS.Desktop.Extensions     5      ��5
      ��   �� RelativeSource  �� *FindAncestor+ �� AncestorType" � �� Path$ ActualWidth���5      ��   �� *FindAncestor+" �$ ActualHeight� � 	Resources 5   
    �� MergedDictionaries<  6ArcGIS.Desktop.Extensions.DesignOnlyResourceDictionary   Source�� 5   
    5        5      $S Mpack://application:,,,/ArcGIS.Desktop.Framework;component\Themes\Default.xaml?�6:   %    5      .��5      �  � RowDefinitions 5   	   �� 5      $��* �6   �� 5      $
��Auto �6   ��5      �� 5       �� ImageSource$ ../Images/background.jpg��6    �� Opacity$	 0.5Q�6:   .+�5      � $��0��6   $��20q�6   $��Center=�6$   $��Center=�6A    (  $Esri_DialogClientAreaBackgroundBrush#���   6\   ��5      �� 5   
   	 �� 
TargetType"	 �6   .�5       �� 
 �� Property
 �@J�Opacity �� Value$	 0.8Q�6&   64   5!       5#      �� 5$   
   $��* �6   �� 5%   
   $��* �6   .+�5'      �� $��0��6   $*��$Select a Project to start ArcGIS Pro��6     Esri_TextBlockH1#���  6K   $��Center=�6v   $��10q�6�   6�   � 5(      $��1��6   $��Center=�6    � ColumnDefinitions 5)   
   �� 5*      $	��20* �6   6+   �� 5+      $	��15* �6   6+   .+�5-   
   �� -   5        $��ListProjects��5-      $��0��6&    �� DisplayMemberPath$
 Name��5.       " Esri_ListBoxItemHighlightBrush#���  5/      $��
10,10,0,10q�50      $	��200��52      $	��200��53      $��Center=�55      ��5-   6   �� *ProProjects+ �� SelectedItem 51      �� *SelectedProjectFile+/   )PG__acara_2_task_2_frendy.UI.OnMouseClick   MouseLeftClick 54      �� *OpenSelectedProjectCommand+ J� InputBindings 56      �� 57      
 �� Key$ Enter��6    �� Command 6%   �� *OpenSelectedProjectCommand+6V   59      �� 5:   
   $��1��6   $��Center=�5;      $��10q�60     Esri_Button#���  6<   ��5:   !   �� *OpenProjectCommand+��5<      �� 5=      ._�5>      ��  �� Orientation$ 
Horizontal=�6   .+�5?      �� $%��../Images/FolderOpenState32.png��6   $��10q�6A   $��Center=�6M   $
��None=�5@   $   65   �� 5A      $��Center=�6   $��Browse��69   $��Center=�5B      $��14(�67   $��5,5,10,5q�5C      5D      5E      5G      5H   	   5I      �� 5J      $��1��6   $��60q�6   $ Vertical=�6*   .+�5K      �� #���  6   
������ $��20��5L   W   $
��Left=�6e   ��6,   �� *AboutArcGISProCommand+��5M      �� 5N      $ 
Horizontal=�6!   .+�5O      �� $��../Images/CogWheel24.png��6    $��3q�6B   $
��Left=�5P   #   $��Center=�6>   $
��None=�5Q   #   64   �� 5R      $��Center=�6$   $��Settings��6A   $��Center=�5S      $��14(�66   $��3,5,10,5q�5T        H5TextBlock#���  6-   5U      5W      5X      5Y      5Z      