/* 
    ------------------- Code Monkey -------------------
    Code provided by unitycodemonkey.com

    Adapted to 2022 Unity (TMP) & Simplified for my needs
    --------------------------------------------------
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HighscoreTable : MonoBehaviour {

    private Transform entryContainer;
    private Transform entryTemplate;
    private List<Transform> highscoreEntryTransformList;
    private int count = 0;

    private void Awake() {
        entryContainer = transform.Find("highscoreEntryContainer");
        entryTemplate = entryContainer.Find("highscoreEntryTemplate");
        entryTemplate.gameObject.SetActive(false);
        //PlayerPrefs.DeleteKey("highscoreTable"); // Delete database for testing

        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        if (highscores == null) {
            // There's no stored table, initialize
            highscores = new Highscores() {
                highscoreEntryList = new List<HighscoreEntry>()
            };
        }
        //AddHighscoreEntry(1, "TO BEAT"); // Test entry highscore

        // Sort entry list by Score
        for (int i = 0; i < highscores.highscoreEntryList.Count; i++) {
            for (int j = i + 1; j < highscores.highscoreEntryList.Count; j++) {
                if (highscores.highscoreEntryList[j].score > highscores.highscoreEntryList[i].score) {
                    // Swap
                    HighscoreEntry tmp = highscores.highscoreEntryList[i];
                    highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
                    highscores.highscoreEntryList[j] = tmp;
                }
            }
        }


        // Limit list to show 10 entry
        highscoreEntryTransformList = new List<Transform>();
        foreach (HighscoreEntry highscoreEntry in highscores.highscoreEntryList) {
            CreateHighscoreEntryTransform(highscoreEntry, entryContainer, highscoreEntryTransformList);
            count++;
            if (count > 9){
                break;
            }
        }

    }

    // Function that create clone of my template I made from the highscoreTable
    private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList) {
        float templateHeight = 75f;
        // Use the container and template
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        // Prep the rank, name, score to be added in text
        int rank = transformList.Count + 1;
        string rankString = rank.ToString();
        int score = highscoreEntry.score;
        string name = highscoreEntry.name;

        // Script to find element of the clone, and to fill rank, name, score
        TextMeshProUGUI textComponentPos = entryTransform.Find("posText").GetComponent<TextMeshProUGUI>();
        textComponentPos.SetText(rankString);

        TextMeshProUGUI textComponentScore = entryTransform.Find("scoreText").GetComponent<TextMeshProUGUI>();
        textComponentScore.SetText(score.ToString());

        TextMeshProUGUI textComponentName = entryTransform.Find("nameText").GetComponent<TextMeshProUGUI>();
        textComponentName.SetText(name);

        // Add the entry to the canvas to show to player
        transformList.Add(entryTransform);
    }

    public static void AddHighscoreEntry(int score, string name) {
        // Create HighscoreEntry
        HighscoreEntry highscoreEntry = new HighscoreEntry { score = score, name = name };
        
        // Load saved Highscores
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        if (highscores == null) {
            // There's no stored table, initialize
            highscores = new Highscores() {
                highscoreEntryList = new List<HighscoreEntry>()
            };
        } 

        // Add new entry to Highscores
        highscores.highscoreEntryList.Add(highscoreEntry);

        // Save updated Highscores
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
    }

    private class Highscores {
        public List<HighscoreEntry> highscoreEntryList;
    }

    /*
     * Represents a single High score entry
     * */
    [System.Serializable] 
    private class HighscoreEntry {
        public int score;
        public string name;
    }

}
