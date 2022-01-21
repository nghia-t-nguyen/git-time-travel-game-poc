using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    private Rigidbody2D body;
    [SerializeField] private float speed;
    private float scaling_factor;
    private Animator anim;
    private Vector2 target;
    private float horizontalInput;

    private void Awake() {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        scaling_factor = 2;
        transform.localScale = Vector3.one*scaling_factor;
    }

    private void Update() {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0)) {
            bool mouseClickInInputField = mousePos.x > 3.71f && mousePos.x < 11.36f
                && mousePos.y < -2.16f && mousePos.y > -2.57f;
            if (System.Math.Abs(body.position.x-mousePos.x) > 1.5f && !mouseClickInInputField)
                target = new Vector2(mousePos.x, 0);
            else
                target = transform.position;
        }

        transform.position = Vector2.MoveTowards(transform.position, target, Time.deltaTime * 5f);

        horizontalInput = target.x - transform.position.x;

        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one*scaling_factor;
        else if (horizontalInput < -0.01f)
            transform.localScale =  new Vector3(-1,1,1)*scaling_factor;

        anim.SetBool("run", horizontalInput != 0);
    }
}
