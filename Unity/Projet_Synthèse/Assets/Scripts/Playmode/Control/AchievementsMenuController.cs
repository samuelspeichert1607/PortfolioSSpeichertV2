using Harmony;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ProjetSynthese
{
    [AddComponentMenu("Game/Control/AchievementsMenuController")]
    public class AchievementsMenuController : GameScript, IMenuController
    {
        [SerializeField]
        [Tooltip("The gameObject that will contain all the achievements")]
        private GameObject achievementsScroll;

        [SerializeField]
        [Tooltip("A prefab of an interface made to show an achievement")]
        private GameObject achievementPrefab;

        [SerializeField]
        [Tooltip("array of all the images of the achievements")]
        private Sprite[] achievementImages;

        private PlayerInputSensor playerInputSensor;

        private ActivityStack activityStack;

        private GameActivityParameters gameActivityParameters;

        private AchievementsInfo achievementsInfo;

        private AchievementRepository achievementRepository;

        private void InjectAchievementsMenuController([ApplicationScope] ActivityStack activityStack,
                                                      [ApplicationScope] GameActivityParameters gameActivityParameters,
                                                      [ApplicationScope] PlayerInputSensor playerInputSensor,
                                                      [EntityScope] AchievementRepository achievementRepository)
        {
            this.activityStack = activityStack;
            this.gameActivityParameters = gameActivityParameters;
            this.playerInputSensor = playerInputSensor;
            this.achievementRepository = achievementRepository;
        }

        private void Awake()
        {
            InjectDependencies("InjectAchievementsMenuController");
        }

        private void Start()
        {
            achievementsInfo = achievementsScroll.GetComponentInChildren<AchievementsInfo>();
            achievementsScroll.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
            achievementsScroll.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
        }

        private void OnEnable()
        {
            playerInputSensor.Players[0].OnUp += OnUp;
            playerInputSensor.Players[0].OnDown += OnDown;
            playerInputSensor.Players[0].OnBuild += OnExit;
        }

        private void OnDisable()
        {
            playerInputSensor.Players[0].OnUp -= OnUp;
            playerInputSensor.Players[0].OnDown -= OnDown;
            playerInputSensor.Players[0].OnBuild -= OnExit;
        }

        public void OnCreate(params object[] parameters)
        {
            //on load tous les achievements
            List<Achievement> achievements = (List<Achievement>)achievementRepository.GetAllAchievements();
            for (int i = 0; i < achievements.Count; i++)
            {
                GameObject achievement = Instantiate(achievementPrefab, achievementsScroll.transform);

                //on set le bottom de l'achievement
                Vector2 newAnchorMin = new Vector2(achievement.GetComponent<RectTransform>().anchorMin.x,
                    achievement.GetComponent<RectTransform>().anchorMin.y - achievementsInfo.AchievementHeight * i);
                achievement.GetComponent<RectTransform>().anchorMin = newAnchorMin;

                //on set le top de l'achievement
                Vector2 newAnchorMax = new Vector2(achievement.GetComponent<RectTransform>().anchorMax.x,
                    achievement.GetComponent<RectTransform>().anchorMax.y - achievementsInfo.AchievementHeight * i);
                achievement.GetComponent<RectTransform>().anchorMax = newAnchorMax;

                //on entre toutes les données de l'achievement dans le gameObject
                List<GameObject> children = (List<GameObject>)achievement.GetAllChildrens();
                for (int j = 0; j < children.Count; j++)
                {
                    if (children[j].activeSelf == false) //si c'est le cacheur
                    {
                        if (!achievements[i].IsUnlocked)
                        {
                            children[j].SetActive(true);
                        }
                    }
                    else if (children[j].GetComponent<Image>() != null) //c'est l'image
                    {
                        children[j].GetComponent<Image>().sprite = achievementImages[achievements[i].Id];
                    }
                    else if (children[j].GetComponent<Text>().fontSize == achievementsInfo.AchievementTitleFontSize) //c'est le titre
                    {
                        children[j].GetComponent<Text>().text = achievements[i].Name;
                    }
                    else //c'est la description
                    {
                        if (achievements[i].IsUnlocked)
                        {
                            children[j].GetComponent<Text>().text = achievements[i].Description;
                        }
                    }

                }
            }



        }

        public void OnResume()
        {
            achievementsScroll.transform.localPosition = new Vector2(0, 0);
        }

        public void OnPause()
        {
            //Nothing to do
        }

        public void OnStop()
        {
            //Nothing to do
        }

        private void OnUp()
        {
            ScrollAchievements(achievementsInfo.ScrollSpeed);
        }


        private void OnDown()
        {
            ScrollAchievements(achievementsInfo.ScrollSpeed * -1);
        }

        private void OnExit()
        {
            activityStack.StopCurrentMenu();
        }

        private void ScrollAchievements(float height)
        {
            achievementsScroll.GetComponent<RectTransform>().offsetMax = new Vector2(0, achievementsScroll.GetComponent<RectTransform>().offsetMax.y + height);
            achievementsScroll.GetComponent<RectTransform>().offsetMin = new Vector2(0, achievementsScroll.GetComponent<RectTransform>().offsetMin.y + height);
            if (achievementsScroll.GetComponent<RectTransform>().offsetMin.y < 0)
            {
                achievementsScroll.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
                achievementsScroll.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
            }
            else if (achievementsScroll.GetComponent<RectTransform>().offsetMin.y > achievementsInfo.MaxScrollHeight)
            {
                achievementsScroll.GetComponent<RectTransform>().offsetMax = new Vector2(0, achievementsInfo.MaxScrollHeight);
                achievementsScroll.GetComponent<RectTransform>().offsetMin = new Vector2(0, achievementsInfo.MaxScrollHeight);
            }
        }
    }
}
