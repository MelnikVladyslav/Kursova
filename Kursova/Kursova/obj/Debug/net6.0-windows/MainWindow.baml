   M S B A M L   `   `   `  ����  Kursova    Kursova.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	     � Title$  Simplex Method Solver��5   	   $	��500��6'   $	��850��64   
�������.��5	      � ��5   
   �� 5       �� 
StartPoint$	 0,0&�6"    �� EndPoint$	 1,1&�63   .��5      �  � Color$ #FFB6C1��6    � Offset$	 0.0Q�6/   � 5      $ 	#FFF5F5F5��6   $	 1.0Q�61   5      .+�5   
   o� $	��Top=�6   ���	#FFEBEBEB���#333.@�5      m� $��_File��6   .@�5      m� -   5        $��_Open��5      6*   m� 5      -   5        $��_Save��5      6*   �� 5      m� 5      -   5        $��_Exit��5      6*   5      m� 5      $��_Task��6   .@�5      m� -   5        $��_Solve Simplex��5      63   m� 5      -   5        $��_Reset��5      6+   5      m� 5      $��_Help��6   .@�5      m� -   5        $��_About��5      6+   5       5!      �� 5$   
    �� CornerRadius$ 10��6   
������� �� Padding$ 20q�66   $��30,60,30,30q�6C    J� Effect 5%      3  -System.Windows.Media.Effects.DropShadowEffect  5&         Color$ Black��6#   	   
BlurRadius$	 10Q�61   
   ShadowDepth$
 5Q�6A   ._�5)      ��  �� Orientation$ Vertical=�6   $��Center=�60   .+�5,      �� $��Simplex Method Solver��6   $��22(�69   $
��Bold"�6G   ���#333$��Center=�5-   .   $��0,0,0,10q�6K   �� 50      -   5         ��Name$ InputTextBox��50      $	��100��60   $	��700��6=   $#��Enter simplex tableau here...��51       �� AcceptsReturn$
 True��6?   $��14(�52      ���#F0F0F0���#333$��10q�53       �� TextWrapping$
 Wrap=�6'   " �� VerticalScrollBarVisibility$
 Auto=�6;   �� 56      -   5        $��Run Simplex Method��56      $	��200��66   $��40��6B   $��0,15,0,0q�6N   ���	#FF6A5ACD
�������$��16(�57   C   $
��Bold"�6Q   $��Center=�58      66   �� 5;      ���#F0F0F0$ 10q�6.   $��0,20,0,0q�6;   $ 5��6M   ._�5<      �� -	   5         ��Name$ ResultTextBlock��5<       $"��Solution will appear here...��69   $��14(�5=       ���#333$
��Wrap=�6@   5>      5?      5@      5A      5B      