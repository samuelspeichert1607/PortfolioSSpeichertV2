// question
draw_text(obj_question.x,obj_question.y,argument0)

// réponse 1
draw_text(obj_button_1.x+16,obj_button_1.y+16,argument1)

// réponse 2
draw_text(obj_button_2.x+16,obj_button_2.y+16,argument2)

// réponse 3
draw_text(obj_button_3.x+16,obj_button_3.y+16,argument3)

// réponse 4
draw_text(obj_button_4.x+16,obj_button_4.y+16,argument4)

//réponse choisie
global.good_answer = argument5

draw_set_font(font0);

//réponse justifiée
draw_text_color(obj_reponse.x,obj_reponse.y,argument6,c_red,c_red,c_white,c_white,1)

draw_set_font(font0);
