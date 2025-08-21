using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    public Texture2D levelMap;
    public Texture2D aiMap;
   
    public PixelToObject[] pixelColourMappings;
    public Color pixelColour;
    public Color pathColour;
    public int tileSize = 4;

    public void Start()
    {
        GenerateLevel(levelMap);
        GenerateLevel(aiMap);
    }
    public void GenerateLevel(Texture2D mapTexture)
    {
     
        //scan whole texture and get positions of objects
        //loops through each pixel in texture map
        
        for (int x = 0; x < mapTexture.width; x++)
        {
            for (int y = 0; y < mapTexture.height; y++)
            {
                //for each pixle, it calls GernerateObject(x,y,maptexture) to create the corresponding object(title) based on pixle colour
                GenerateObject(x,y, mapTexture);
            }
        }
    }
    public void GenerateObject(int x, int y, Texture2D map)
    {

        //reads the pixle colour at(x, y) on the map using map.GetPixle(x, y)
        pixelColour = map.GetPixel(x, y);
        //if the pixel is fully tranparent (alpha = 0)
        if (pixelColour.a == 0)
        {
            //it skips prosessing
            return;
        }
        //otherwise the pixle is non-transparent
        else
        {
            //iterates through the pixelColourMapping array
            foreach (PixelToObject colourPair in pixelColourMappings)
            {
                //Scan pixelColourMappings Array for matching colour mapping
                if (colourPair.pixelColour.Equals(pixelColour))
                {
                    Vector3 positionToSpawn = new Vector3(x+(tileSize*x), 0, y +(tileSize*y));
                    // instantiate the corresponding prefab at the correct position
                    Instantiate(colourPair.prefab, positionToSpawn, Quaternion.identity, colourPair.parent);

                }
            }

            
           
        }

        


    }
}
[System.Serializable]
public class PixelToObject
{
    public Color pixelColour;
    public GameObject prefab;
    public Transform parent;
}