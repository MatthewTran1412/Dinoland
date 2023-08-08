using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class Creature : MonoBehaviour
{
    public int maxhp;
    public int currenthp;
    public bool isHurt{get;private set;}
    [SerializeField]public Slider m_Slider;
    [SerializeField]private Image m_FillImage;
    [SerializeField]private Color m_FullHealthColor=Color.green;
    [SerializeField]private Color m_ZeroHealthColor=Color.red;
    [SerializeField]private float maxfood;
    [SerializeField]private float maxwater;
    [SerializeField]public float currentfood;
    [SerializeField]public float currentwater;
    [SerializeField]private Text t_food;    
    [SerializeField]private Text t_water;
    [SerializeField]private Image m_FoodImage;
    [SerializeField]private Sprite[] foodSprite;
    private void OnEnable() {
        if(GameManager.Instance)
            GameManager.Instance.e_GamePlayed+=SetHealthUI;
    }
    private void OnDisable() =>GameManager.Instance.e_GamePlayed-=SetHealthUI;
    private void Start() {
        currenthp=maxhp;
        m_Slider=transform.Find("Canvas").transform.Find("HealthSlider").GetComponentInParent<Slider>();
        m_FillImage=transform.Find("Canvas").transform.Find("HealthSlider").transform.Find("Fill Area").transform.Find("Fill").GetComponentInParent<Image>();
        m_Slider.gameObject.SetActive(false);
        currentfood=maxfood;
        currentwater=maxwater;
        m_FoodImage=GameObject.Find("food").GetComponent<Image>();
        t_food=GameObject.Find("t_food").GetComponent<Text>();
        t_water=GameObject.Find("t_water").GetComponent<Text>();
        if(GetComponent<Animation>())
            m_FoodImage.sprite=GetComponent<TargetType>().m_Type==TargetType.Type.Carnivore?foodSprite[0]:foodSprite[1];
    }
    public void DealDamage(int amount)
    {
        if(gameObject.GetComponent<Animation>())
            m_Slider.gameObject.SetActive(true);
        currenthp-=amount;
        isHurt=true;
        StartCoroutine(ReturnFalse());
        if(amount>=(maxhp/3))
            GetComponent<Animator>().SetTrigger("Knockdown");
        if(currenthp<=0)
        {
            GetComponent<Animator>().SetTrigger("Death");
            GetComponent<TargetType>().m_Type=TargetType.Type.Meat;
            if(GetComponent<Animation>())
            {
                PlayerControl.Instance.enabled=false;
                GameObject.Find("Canvas").transform.Find("Revive").gameObject.SetActive(true);
            }
        }
    }
    private IEnumerator ReturnFalse()
    {
        yield return new WaitForSeconds(5);
        isHurt=false;
    }
    private void SetHealthUI()
    {
        m_Slider.value=currenthp;
        m_FillImage.color=Color.Lerp(m_ZeroHealthColor,m_FullHealthColor,currenthp/maxhp);
        if(!GetComponent<Animation>())
            return;
        currentfood=currentfood>=maxfood?maxfood:currentfood;
        currentwater=currentwater>=maxwater?maxwater:currentwater;
        t_food.text=Mathf.Round(currentfood)+" / "+maxfood;
        t_water.text=Mathf.Round(currentwater)+" / "+maxwater;
        currentfood-=(Time.deltaTime/5);
        currentwater-=(Time.deltaTime/5);
        currenthp-=currentfood<=0 || currentwater<=0?(int)(Time.deltaTime/2):0;
    }
}














