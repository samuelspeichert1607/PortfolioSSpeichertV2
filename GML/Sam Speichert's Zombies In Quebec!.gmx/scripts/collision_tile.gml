/*
Use:        collision_tile(layer,direction)

layer:      This is the layer the interactive tiles are located on
direction:  This is the arrow key direction to check (e.g., vk_right)


How it works
============================================
Object collisions have to check the bounding boxes of all objects' sprites. This script
only requires the calling object's bounding box. Tiles have their own bounding boxes as
defined in the background sheet's properties, as well as the size of the tile when placed
in the room if Shift+Click/Drag selection is used to override the default tile sizes.
Tile-based collision checking should in theory be much faster.

Checking full sprite collision for the calling object is wasteful, so first the direction
needs to be specified. For this, you can use the arrow keys: vk_up, vk_down, vk_right, and 
vk_left. Each arrow key corresponds to a bbox face: bbox_top, bbox_bottom, bbox_right and
bbox_left respectively. This significantly reduces the number of pixels needed to be checked.
Of course, we need to specify which pixels, so for that we use a FOR loop. 

Again, we want to cut down the number of pixels checked, so we increment the value of the FOR 
loop by 2, 4 or 8, depending on the dimensions of the sprite's bounding box. By default, the 
value is incremented by 2, but ideally a sprite would have bounding box dimensions in multiples 
of 8 (e.g, [0,0,15,31]), since the typical tile is 16x16 pixels. A basic tile is only 8x8px,
but they are usually arranged in 16x16px metatiles (and the NES further arranged them into
32x32px tile square assemblies, but don't worry about that for now).

In case a tile is misaligned or tiles are only used sparsely, we need to check that there
actually is a tile at the specified location. For that we use the tile_exists() function.
The ID of the tile is of course specified by tile_layer_find() using the interactive tile
layer for argument0, the specified bbox face for the X-coordinate in the room, and the value
currently active in the FOR loop for the Y-coordinate (or conversely, the FOR loop value is 
used for the X-coordinate and the bbox face is used for the Y-coordinate when checking above
or below the calling object).

There are actually multiple ways to check if a tile is interactive. This script uses the
function tile_get_top() with the expectation that all interactive tiles are on the same row
and have coordinates in the tilesheets at (0,y), but it's not the only method. You can replace 
tile_get_top() with the following functions (these are given as examples):

    tile_exists()       
    tile_get_depth()
        These both function the same, but tile_get_depth() would be redundant, since by not
        expanding the tile_exists() conditional, you're just checking the tile depth.

    tile_get_background()
        Store interactive tiles all in one tilesheet, then call this function.

    tile_get_left()
        Same as tile_get_top, but you'd put all the interactive tiles in the same column.
        
    tile_get_visible()
        Not recommended, as it would require placing invisible tiles over visible tiles.

If there is a tile that meets the desired criteria, the script will return TRUE (1). In 
other words, this script will tell you if there is a tile collision. If you want to check
that there is NOT a tile collision, use !collision_tile(layer,direction) instead.

=============================================

Credit:     Theou Aegis
Contact:    snarl_los@yahoo.com
*/

switch argument1
{

case ord("A"):
    for (tile=bbox_top;tile<=bbox_bottom;tile+=2)
        if tile_exists(tile_layer_find(argument0,bbox_left-1,tile))
            if tile_get_top(tile_layer_find(argument0,bbox_left-1,tile))=0
            {
                return 1;
                break;
            }
    break;

case ord("D"):
    for (tile=bbox_top;tile<=bbox_bottom;tile+=2)
        if tile_exists(tile_layer_find(argument0,bbox_right+1,tile))
            if tile_get_top(tile_layer_find(argument0,bbox_right+1,tile))=0
            {
                return 1;
                break;
            }
    break;
            
case ord("W"):
    for (tile=bbox_left;tile<=bbox_right;tile+=2)
        if tile_exists(tile_layer_find(argument0,tile,bbox_top-1))
            if tile_get_top(tile_layer_find(argument0,tile,bbox_top-1))=0
            {
                return 1;
                break;
            }
    break;

case ord("S"):
    for (tile=bbox_left;tile<=bbox_right;tile+=2)
        if tile_exists(tile_layer_find(argument0,tile,bbox_bottom+1))
            if tile_get_top(tile_layer_find(argument0,tile,bbox_bottom+1))=0
            {
                return 1;
                break;
            }
    break;
}
