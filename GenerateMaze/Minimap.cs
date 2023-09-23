using UnityEngine;
using UnityEngine.UI;

public class Minimap : MonoBehaviour {
    public RenderTexture minimapTexture;
    public RawImage minimapImage;

    public Camera cam;

    public void MakeMinimap(char[,] map){
        Texture2D texture = new Texture2D(map.GetLength(0), map.GetLength(1), TextureFormat.RGBA32, false);
        RenderTexture.active = minimapTexture;
        texture.ReadPixels(new Rect(0, 0, map.GetLength(0), map.GetLength(1)), 0, 0);
        texture.Apply();
        print(map.GetLength(0));
        for(int i=0; i<map.GetLength(0); i++){
            for(int j=0; j<map.GetLength(1); j++){
                if (map[i, j] == 'N'){
                    Color color = Color.green;
                    texture.SetPixel(i, j, color);
                }
                else if (map[i, j] == 'S'){
                    Color color = Color.yellow;
                    for(int k=0; k<3; k++){
                        for(int l=0; l<3; l++){
                            texture.SetPixel(i+k, j+l , color);
                        }
                    }
                }
                else if(map[i, j] == ' '){
                    Color color = Color.white;
                    texture.SetPixel(i, j, color);
                }
                else if(map[i, j] == 'B'){
                    Color color = Color.blue;
                    for(int k=0; k<3; k++){
                        for(int l=0; l<3; l++){
                            texture.SetPixel(i+k, j+l , color);
                        }
                    }
                }
                else if(map[i, j] == 'E'){
                    Color color = Color.red;
                    for(int k=0; k<3; k++){
                        for(int l=0; l<3; l++){
                            texture.SetPixel(i+k, j+l , color);
                        }
                    }
                }
                else if(map[i, j] == 'I'){
                    Color color = Color.black;
                    for(int k=0; k<2; k++){
                        for(int l=0; l<2; l++){
                            texture.SetPixel(i+k, j+l , color);
                        }
                    }
                }
            }
        }
        texture.Apply();
        Graphics.Blit(texture, minimapTexture);
        print("test");
        minimapImage.texture = texture;
        cam.targetTexture = minimapTexture;
        print("helo");
    }


}