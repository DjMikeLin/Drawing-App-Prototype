# Drawing-App-Prototype
Based off Homework 4 from Windowing Systems Programming. This prototype adds additional features consisting of additional shapes. 
Original homework requirements will be attached as a PDF. And, at a later time, mvvm, save, load, more shapes, and etc will be added.

Current Features:
Available shapes to draw: Rectangle, any ellispe including circles, straight lines, dots, pencil draw, right triangle, and triangle in 
exact order in Shape combobox. 

Colors: All colors in the SolidColorBrush class in wpf are available for fill color and stroke color. 

Stroke Thickness Setting:  stroke thickness can be changed via a textbox with any integer. User will be prompted to enter another character
each time a non-integer char is entered into the textbox. 

----Main currently contains 3 buttons with corresponding key inputs that allow users to do the same thing as clicking it. 

Reset lets the user start over with a new drawing Canvas. Exit allows user to terminate app. Lastly, save does not work at this time. 

Future updates/commits will add save, load, more shapes, and etc to the app. 

Update 1:
- Added Save As, Save, Open functions under Main. 
- Added in MVVM, MVC, and an Icons folder containing shapes for the shapes combobox. 
    !!!!!Some functions shouldn't be in my MVVM at this current point like Exit.!!!!!!
- Current Features:
  Available shapes to draw: Rectangle, any ellispe including circles, straight lines, dots, pencil draw, right triangle, equilateral         triangle, diamond(rhombus), any number of sides polygon, rounded edge rectangle, and arcs in exact order in Shape combobox. 
    >>>>>>>Currently Polygon ONLY completes the shape(i.e. fill in shape and stroke) upon switching shapes. i.e. the last endpoint of the 
           point on the polygon will connect with the initial starting point of the polygon and make the shape. 
- Added Eraser. Eraser changes size based on stroke size typed in by the user. Checked = eraser, Unchecked = regular options. 
- Added in X and Y cords of current mouse position in canvas at the bottom left side. 
- Added in tooltips for all shapes in combo box, and eraser checkbox to improve usability. 

Possible future updates/commits will possibly add funtions like textbox and selecting different regions of the canvas to copy and paste. Possible fix of MVVM and MVC to how it was meant to be by definition by removing functions like Exit from MVVM. 
