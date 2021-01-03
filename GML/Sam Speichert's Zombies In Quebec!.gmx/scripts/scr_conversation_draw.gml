draw_sprite(argument0,0,view_xview[0]+0,view_yview[0]+504) // fenetre rpg

for(i = 0; i < string_length(argument1); i++)
{
  if i < string_length(argument1)
  {
  draw_text(view_xview[0]+168,view_yview[0]+504,string_copy(argument1,i,string_length(argument1)))  //texte
  i++;
  }
}

draw_sprite(argument2,0,view_xview[0]+0,view_yview[0]+550) // sprite du personnage
draw_text(view_xview[0]+0,view_yview[0]+504,argument3) // nom du personnage
//Image présentée, sera à 0 si rien
depth = -99999999
draw_set_font(font0);
