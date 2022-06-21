using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankingListController : MonoBehaviour
{
    RankingSongContainer ranking;
    public string rankingLabel;
    public RankingUIElement elementRef;
    [SerializeField] float offset = 20F;

    private void Start()
    {
        rankingLabel = PlayerPrefs.GetString("RankingFile", "RankingKakegurui");
        string path = Application.streamingAssetsPath + "/RankingsData/" + rankingLabel + ".xml";

        ranking = RankingSongContainer.LoadRaking(path);

        DisplayRanking();

    }

    public void DisplayRanking()
    {
        float posY = 141F;
        for (int i = 0; i < ranking.data.Count; i++)
        {
           // print(ranking.data[i].initials[0] + "." + ranking.data[i].initials[1] + "."
            //    + ranking.data[i].initials[2] + " = " + ranking.data[i].value);
            GameObject element = Instantiate(elementRef.gameObject, this.transform, false);
            RectTransform r_trans = element.GetComponent<RectTransform>();
            r_trans.localPosition = new Vector3(r_trans.localPosition.x, posY - ( offset * i ), r_trans.localPosition.z);
            RankingUIElement el_ranking = element.GetComponent<RankingUIElement>();
            el_ranking.SetInitial(ranking.data[i].initials);
            el_ranking.SetRecordValue(ranking.data[i].value);
        }
    }
}
