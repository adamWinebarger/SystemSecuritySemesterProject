   M S B A M L   `   `   `  ����I  EBAARS 4 Tester, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:BAARS_4_TesterBAARS_4_Tester  5   	       BAARS_4_Tester.MainWindow   5   	   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:BAARS_4_Tester     5   	   -   5          �Name$  Main_Window��5   	    � Title$ 
MainWindow��5   	   $	��450��6   $	��815��6)    � WindowStartupLocation$ CenterOwner=�65   .��5	      � .+�5
   
   �� $�� BAARS-IV Scoring Program��6   $��Center=�64   $	��Top=�6Q    J� RenderTransformOrigin$ 0.442,0.048&�6i   $��30(�6�   $	��800��6�   $
��Bold"�6�   $��2��6�   ���	#FFDDDDDD$��51��6�   �� 5   
   -   5         ��Name$ TakeTestButton��5      $��
New Tester��6)   $
��Left=�6>   $��
134,64,0,0q�6Y   $	��Top=�6m   $��46��6�   $	��115��6�   6�   �� 5   
   -   5        $ QuickScoreButton��5      $��Two \nLines��6+   $
��Left=�6A   $��	10,64,0,0q�6\   $	��Top=�6o   $��46��6�   $	��112��6�   6�   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ Table��5      $��10,149,10,10q�6"   $	��780��68      	RowHeight$ 20Q�6D   $��Stretch=�6S      MinColumnWidth$ 40Q�6r   
�������   RowBackground �	#FFDDDDDD	   AlternatingRowBackground	 �	#FFFFFFFF
   
IsReadOnly$

 True��6�      CanUserReorderColumns$ False��6�      CanUserResizeColumns$ False��6     CanUserResizeRows$ False��6)     CanUserSortColumns$ False��6C   � 	Resources 5      )  #System.Windows.Controls.DataGridRow �� 
TargetType��%E   '        �� 5      "  6   .�5      -   5        5      5      5      �� 5   
   -   5         ��Name$ 	SearchBox��5      $
��Left=�6%   $��19��6@   $��
10,121,0,0q�6L    �� TextWrapping$
 Wrap=�6`   $	��Top=�6t   $	��239��6�   
�������6�   �� 5   
   -   5         ��Name$ lastNameCheckBox��5      $��	Last Name��6-   $
��Left=�6A   $��618,122,0,0q�6\   $	��Top=�6q   6�   �� 5   
   -   5        $ firstNameCheckBox��5      $��
First Name��6.   $
��Left=�6C   $��413,122,0,0q�6^   $	��Top=�6s   $ 0.21,-0.593&�6�   6�   �� 5   
   -	   5        $ middleNameCheckBox��5      $��Middle Name��6/   $
��Left=�6E   $��507,122,0,0q�6`   $	��Top=�6u   $ 0.572,0.157&�6�   �� 5   
   -
   5        $ RefreshButton��5      $��Refresh��6(   $
��Left=�6:   $��
730,95,0,0q�6U   $	��Top=�6i   $��20��6�   $��60��6�   �� 5   
   -   5        $ DeleteButton��5      $��Delete��6'   $
��Left=�68   $��730,121,0,0q�6S   $	��Top=�6h   $��20��6�   $��60��6�   $ 0.589,-0.065&�6�   �� 5   
   -   5        $  ToggleAdvancedSearchButton��5      $��Advanced Search Options��65   $
��Left=�6W   $��259,120,0,0q�6r   $	��Top=�6�   $	��142��6�   �� 5   
   -   5        $ showResultsButton��5      $��Show Results��6,   $
��Left=�6C   $��
259,64,0,0q�6^   $	��Top=�6r   $��46��6�   $	��100��6�   6�   5      5      