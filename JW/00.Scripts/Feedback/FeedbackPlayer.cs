using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FeedbackPlayer : MonoBehaviour
{
    private List<IFeedback> feedbacks = new List<IFeedback>();

    private void Awake()
    {
        feedbacks = GetComponentsInChildren<IFeedback>().ToList();
    }

    public void PlayFeedbacks()
    {
        feedbacks.ForEach(f => f.PlayFeedback());
    }
}
