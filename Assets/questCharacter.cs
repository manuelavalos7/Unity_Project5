using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class questCharacter : MonoBehaviour
{
    public Quest[] quests = { new Quest(), new Quest(), new Quest()};
    private playerMovement Player;
    public bool player_near = false;
    public int current_quest = 0;

    // Start is called before the first frame update
    void Start()
    {
        quests[current_quest].trigger.tag = quests[current_quest].questName;
        quests[current_quest].trigger.SetActive(false);
        gameObject.tag = quests[current_quest].questName;
        quests[current_quest].quest_completed = false;
        Player = GameObject.Find("Player").GetComponent<playerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (current_quest < quests.Length)
        {
            if (Vector3.Distance(GameObject.Find("Player").GetComponent<Transform>().position, transform.position) < 5)
            {
                if (!quests[current_quest].quest_completed && !player_near)
                {
                    if (Player.quests.ContainsKey(quests[current_quest].questName))
                    {
                        quests[current_quest].quest_completed = Player.quests[quests[current_quest].questName];
                    }

                    if (!quests[current_quest].greeted)
                    {
                        greet(quests[current_quest].greeting);
                        Player.quests.Add(quests[current_quest].questName, false);
                        quests[current_quest].greeted = true;
                        quests[current_quest].trigger.SetActive(true);
                    }
                    else if (!player_near)
                    {//if player was not already near
                        greet("Don't forget to " + quests[current_quest].quest_objective);
                    }

                    if (quests[current_quest].quest_completed)
                    {
                        GameObject.Find("speechBox").GetComponentInChildren<Text>().text = quests[current_quest].quest_thank;
                        Player.increase_points(10);
                        current_quest++;
                        
                    }
                }
                player_near = true;



            }
            else
            {
                player_near = false;
            }

            if (!player_near)
            {
                GameObject.Find("speechBox").GetComponentInChildren<Text>().text = "";
            }
        }
     
    }

    void greet(string message) {
        GetComponentInChildren<Animator>().SetTrigger("Greet");
        if (!quests[current_quest].quest_completed)
        {
            GameObject.Find("speechBox").GetComponentInChildren<Text>().text = message;
        }
        
    }

    [System.Serializable]
    public class Quest
    {
        public string questName;
        public string greeting;
        public string quest_objective;
        public string quest_thank;
        public int quest_index;
        public GameObject trigger;

        public bool quest_completed;
        public bool greeted = false;

    }

}
