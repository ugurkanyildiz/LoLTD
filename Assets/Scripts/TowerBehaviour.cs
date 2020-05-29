using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBehaviour : MonoBehaviour
{
    GameObject target; //bir hedef değişkeni belirledik
    float attackDist = 25;
    [SerializeField] GameObject head;
    [SerializeField] GameObject bullet;
    [SerializeField] float fireRate=0.25f, fireTimer;
    [SerializeField]float damage;
    void Update()
    {
        fireTimer += Time.deltaTime;
        if (target == null)//Hedefimiz olup olmadığını kontrol ettik
        {
            LookForTarget();//Yok ise yeni bir hedef aradık
        }
        else//Eğer hedefimiz var ise
        {
            if (Vector3.Distance(target.transform.position, transform.position) < attackDist)//Hedefimizin hala mesafemizde olup olmadığını kontrol ettik
            {
                head.transform.LookAt(target.transform.position);//Mesafemizdeyse head (Üst kısım) olarak tanımlanan parçayı ona doğru çevirdik
                if (fireTimer>=fireRate)//atış zamanlayıcısı(fireTimer) atış aralığından (fireRate) büyük ise atışı gerçekleştirdik
                {
                    fireTimer = 0;//atış zamanlayıcısını sıfırladık
                    Shoot(); //Atış Methodunu çağırdık
                }
            }
            else//Mesafemizden çıktıysa
            {
                target = null;//hedefi boşalttık
            }
        }
    }
    void Shoot()
    {
        GameObject blt= Instantiate(bullet, head.transform.position, Quaternion.identity);//Tanımlanan mermi objesini oluşturduk 
      
        BulletBehaviour bltBehaviour= blt.GetComponent<BulletBehaviour>();//Eğer oluşturulan merminin içinde BulletBehaviour (kurşun özelliği) varsa içerisine erişip targetini verdik
        if (bltBehaviour!=null)
        {
            bltBehaviour.damage = damage;
            bltBehaviour.target = target;
        }
        RocketBehaviour rctBehaviour =blt.GetComponent<RocketBehaviour>();//Eğer oluşturulan merminin içinde Rocket (roket özelliği) varsa içerisine erişip targetini verdik
        if (rctBehaviour != null)
        {
            rctBehaviour.damage = damage;
            rctBehaviour.target = target;
        }

    }
    void LookForTarget()
    {

        for (int i = 0; i < Spawner.instance.enemies.Length; i++)//Spawner ın içerisinde oluşturduğumuz enemy listesini çektik ve içinde  döndük
        {
            if (Vector3.Distance(Spawner.instance.enemies[i].transform.position, transform.position) < attackDist)//Vector3 classının altındaki Distance methodu ile enemy ile kendi aramızdaki mesafeyi öğrendik bu mesafe saldırı mesafemizdeyse
            {
                target = Spawner.instance.enemies[i];//enemy i hedef belirledik
                break;
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, attackDist);//Karakterin etrafında bir çizgisel küre oluşturduk vuruş mesafesini görebilmek için, Not: Gizmolar play ekranında (Camera tarafından) görünmezler
    }
}
