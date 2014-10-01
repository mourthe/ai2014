//http://www.mapeditor.org/
/*
 * Don´t like it?
 * use jQuery, Prototype, raw XMLHttpRequest, your favorite JS library for it.
 */
var SOURCE_FROM_TILED_MAP_EDITOR = { "height":42,
 "layers":[
        {
         "data":[355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 491, 491, 491, 491, 491, 491, 491, 491, 491, 491, 491, 491, 491, 491, 355, 355, 355, 355, 491, 491, 491, 491, 491, 172, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 491, 495, 495, 495, 495, 495, 491, 495, 495, 495, 495, 495, 495, 491, 355, 355, 355, 355, 491, 495, 495, 495, 355, 172, 355, 355, 355, 491, 491, 491, 491, 491, 491, 491, 491, 491, 491, 491, 355, 355, 355, 355, 491, 495, 495, 495, 495, 495, 491, 495, 495, 495, 495, 495, 495, 491, 491, 491, 491, 491, 491, 495, 495, 172, 172, 172, 355, 355, 355, 491, 355, 355, 355, 355, 355, 355, 355, 355, 355, 491, 355, 355, 355, 355, 491, 495, 495, 491, 495, 495, 491, 495, 495, 495, 491, 495, 495, 491, 495, 495, 495, 495, 355, 495, 495, 172, 172, 172, 355, 355, 355, 491, 355, 355, 355, 101, 101, 101, 355, 355, 355, 491, 355, 355, 355, 355, 491, 491, 491, 491, 491, 491, 491, 491, 491, 491, 491, 491, 491, 491, 491, 495, 495, 495, 355, 495, 495, 495, 355, 172, 355, 355, 355, 491, 355, 355, 355, 101, 101, 101, 355, 355, 355, 491, 355, 355, 355, 355, 491, 101, 101, 101, 101, 101, 101, 101, 101, 101, 101, 101, 101, 491, 491, 495, 495, 495, 355, 355, 355, 355, 355, 172, 355, 355, 355, 491, 355, 355, 355, 101, 101, 101, 355, 355, 355, 491, 355, 355, 355, 355, 491, 101, 101, 101, 101, 101, 101, 101, 101, 101, 101, 101, 101, 491, 495, 495, 495, 495, 355, 495, 495, 495, 355, 172, 355, 355, 355, 491, 355, 355, 355, 101, 101, 101, 355, 355, 355, 491, 355, 355, 355, 355, 491, 101, 101, 101, 101, 355, 355, 355, 355, 101, 101, 101, 101, 491, 491, 491, 491, 491, 491, 495, 495, 495, 355, 172, 355, 355, 355, 491, 355, 355, 355, 355, 355, 355, 355, 355, 355, 491, 355, 355, 355, 355, 491, 101, 101, 101, 101, 355, 355, 355, 355, 101, 101, 101, 101, 491, 355, 355, 355, 355, 491, 495, 495, 172, 172, 172, 355, 355, 355, 491, 491, 491, 491, 491, 491, 491, 491, 491, 491, 491, 355, 355, 355, 355, 491, 101, 101, 101, 101, 355, 355, 355, 355, 101, 101, 101, 101, 491, 355, 355, 355, 355, 491, 495, 495, 495, 355, 172, 355, 355, 355, 355, 355, 355, 355, 355, 491, 355, 355, 355, 355, 355, 355, 355, 355, 355, 491, 101, 101, 101, 101, 101, 101, 101, 101, 101, 101, 101, 101, 491, 355, 355, 355, 355, 491, 495, 495, 495, 355, 172, 355, 355, 355, 355, 355, 355, 355, 355, 491, 355, 355, 355, 355, 355, 355, 355, 355, 355, 491, 101, 101, 101, 101, 101, 101, 101, 101, 101, 101, 101, 101, 491, 355, 355, 355, 355, 491, 355, 355, 355, 355, 172, 355, 355, 355, 355, 355, 355, 355, 355, 491, 355, 355, 355, 355, 355, 355, 355, 355, 355, 491, 491, 491, 491, 491, 491, 491, 491, 491, 491, 491, 491, 491, 491, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 355, 355, 172, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 172, 355, 355, 355, 355, 355, 355, 355, 355, 355, 172, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 172, 355, 495, 495, 495, 355, 355, 495, 495, 495, 495, 355, 355, 172, 355, 495, 495, 495, 495, 495, 495, 495, 355, 172, 355, 495, 495, 495, 495, 495, 495, 495, 355, 495, 495, 495, 495, 495, 355, 355, 355, 355, 172, 491, 491, 495, 495, 355, 355, 495, 495, 495, 172, 172, 172, 172, 355, 495, 495, 495, 495, 495, 495, 495, 355, 172, 355, 495, 495, 495, 495, 495, 495, 495, 355, 495, 495, 495, 495, 495, 355, 355, 355, 355, 172, 355, 495, 495, 495, 355, 355, 495, 495, 495, 495, 355, 355, 172, 355, 495, 172, 495, 495, 495, 172, 495, 355, 172, 355, 495, 495, 495, 495, 172, 495, 495, 355, 495, 495, 172, 495, 495, 355, 355, 355, 355, 172, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 172, 355, 355, 172, 355, 355, 355, 172, 355, 355, 172, 355, 355, 355, 355, 355, 172, 355, 355, 355, 355, 355, 172, 355, 355, 355, 355, 355, 355, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 355, 355, 355, 355, 172, 491, 491, 491, 491, 491, 491, 172, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 172, 355, 355, 355, 172, 355, 355, 355, 355, 355, 172, 355, 355, 172, 355, 491, 355, 355, 355, 172, 355, 355, 355, 355, 172, 491, 495, 495, 495, 495, 491, 172, 355, 495, 495, 495, 355, 355, 495, 495, 495, 355, 172, 355, 495, 495, 172, 495, 355, 495, 495, 355, 172, 355, 495, 172, 495, 491, 495, 495, 355, 172, 355, 355, 355, 355, 172, 491, 495, 495, 495, 495, 491, 172, 355, 495, 495, 495, 355, 355, 495, 495, 172, 172, 172, 355, 495, 495, 495, 495, 355, 495, 495, 355, 172, 355, 495, 495, 495, 491, 495, 495, 355, 172, 355, 355, 355, 355, 172, 491, 495, 495, 495, 491, 491, 172, 172, 172, 495, 495, 355, 355, 495, 495, 495, 355, 172, 355, 355, 355, 355, 355, 355, 495, 172, 172, 172, 355, 355, 355, 355, 491, 495, 172, 172, 172, 355, 355, 355, 355, 172, 491, 495, 495, 495, 495, 491, 172, 355, 495, 495, 495, 355, 355, 495, 495, 495, 355, 172, 355, 495, 495, 495, 495, 355, 495, 495, 355, 172, 355, 495, 495, 495, 491, 495, 495, 355, 172, 355, 355, 355, 355, 172, 491, 495, 495, 495, 495, 491, 172, 355, 495, 495, 495, 355, 355, 495, 495, 495, 355, 172, 355, 495, 495, 172, 495, 355, 495, 495, 355, 172, 355, 495, 172, 495, 491, 495, 495, 355, 172, 355, 355, 355, 355, 172, 491, 491, 491, 491, 491, 491, 172, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 172, 355, 355, 355, 172, 355, 355, 355, 355, 355, 172, 355, 355, 172, 355, 491, 355, 355, 355, 172, 355, 355, 355, 355, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 355, 355, 355, 355, 172, 355, 355, 355, 355, 355, 355, 355, 355, 172, 355, 355, 355, 355, 491, 355, 172, 355, 491, 355, 355, 355, 491, 355, 172, 355, 355, 355, 172, 355, 355, 355, 355, 491, 355, 355, 355, 172, 355, 355, 355, 355, 172, 355, 495, 495, 495, 495, 495, 495, 355, 172, 355, 495, 495, 495, 491, 495, 172, 495, 491, 495, 495, 495, 491, 495, 172, 495, 355, 355, 172, 355, 495, 495, 495, 491, 495, 495, 355, 172, 355, 355, 355, 355, 172, 355, 495, 495, 172, 172, 495, 495, 355, 172, 355, 495, 172, 495, 491, 495, 495, 495, 491, 495, 172, 495, 491, 495, 495, 495, 355, 355, 172, 355, 495, 495, 495, 495, 495, 495, 355, 172, 355, 355, 355, 355, 172, 355, 355, 355, 172, 172, 355, 355, 355, 172, 355, 355, 172, 355, 491, 355, 355, 355, 491, 355, 172, 355, 491, 355, 355, 355, 355, 355, 172, 355, 355, 355, 355, 355, 355, 355, 355, 172, 355, 355, 355, 355, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 355, 355, 355, 355, 172, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 172, 355, 355, 355, 355, 355, 355, 491, 355, 355, 355, 355, 355, 355, 172, 491, 491, 491, 491, 172, 355, 355, 355, 355, 172, 355, 495, 495, 495, 495, 495, 495, 495, 495, 495, 495, 495, 495, 495, 495, 495, 355, 172, 355, 355, 355, 355, 355, 355, 491, 355, 355, 355, 355, 355, 355, 172, 491, 495, 495, 491, 172, 355, 355, 355, 355, 172, 355, 495, 172, 172, 172, 172, 172, 172, 172, 495, 495, 172, 172, 172, 495, 495, 355, 172, 355, 355, 355, 355, 355, 355, 491, 355, 355, 355, 355, 355, 355, 172, 491, 495, 495, 491, 172, 355, 355, 355, 355, 172, 355, 495, 172, 495, 495, 495, 495, 495, 172, 495, 495, 495, 495, 172, 495, 495, 355, 172, 491, 491, 491, 491, 491, 491, 491, 491, 491, 491, 491, 491, 491, 172, 491, 491, 495, 491, 172, 355, 355, 355, 355, 172, 355, 495, 172, 172, 172, 172, 495, 495, 172, 172, 172, 172, 172, 172, 495, 495, 355, 172, 355, 355, 355, 355, 355, 355, 491, 355, 355, 355, 355, 355, 355, 172, 491, 495, 495, 491, 172, 355, 355, 355, 355, 172, 355, 495, 495, 495, 495, 172, 495, 495, 495, 495, 495, 495, 495, 495, 495, 495, 355, 172, 355, 355, 355, 355, 355, 355, 491, 355, 355, 355, 355, 355, 355, 172, 491, 495, 495, 491, 172, 355, 355, 355, 355, 172, 355, 355, 355, 355, 355, 172, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 172, 355, 355, 355, 355, 355, 355, 491, 355, 355, 355, 355, 355, 355, 172, 491, 491, 491, 491, 172, 355, 355, 355, 355, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355],
         "characters": [
			{
			  "name": "Capitão América",
			  "idx":0, 
			  "position": {"i": 4, "j": 12}
			},
			{
			  "name": "Stark",
			  "idx":1, 
			  "position": {"i": 9, "j": 8}
			},
            {
                "name": "Hawkeye",
                "idx": 2,
                "position": { "i": 5, "j": 34 }
            },
            {
                "name": "Hulk",
                "idx": 3,
                "position": { "i": 23, "j": 37 }
            },
            {
                "name": "Thor",
                "idx": 4,
                "position": { "i": 35, "j": 14 }
            },
            {
                "name": "Black Widow",
                "idx": 5,
                "position": { "i": 36, "j": 36 }
            }
		 ],
		 "height":42,
         "name":"Camada de Tiles 1",
         "opacity":1,
         "type":"tilelayer",
         "visible":true,
         "width":42,
         "x":0,
         "y":0
        }],
 "orientation":"orthogonal",
 "properties":
    {

    },
 "renderorder":"right-down",
 "tileheight":20,
 "tilesets":[
        {
         "firstgid":1,
         "image":"Images\/terrain.png",
         "imageheight":1024,
         "imagewidth":1024,
         "margin":0,
         "name":"AI",
         "properties":
            {

            },
         "spacing":0,
         "tileheight":32,
         "tilewidth":32
        }],
 "tilewidth":20,
 "version":1,
 "width":42
}
