using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    private Rigidbody2D body;
    [SerializeField] private float speed;
    private float scaling_factor;
    private Animator anim;
    private Vector2 target;
    private float horizontalInput;
    private bool mouseClickInInputField;

    private void Awake() {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        scaling_factor = 2;
        transform.localScale = Vector3.one*scaling_factor;
    }

    private void Update() {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0)) {
            float mousePosRelativeX = mousePos.x - body.position.x; // relative to player
            float mousePosRelativeY = mousePos.y - body.position.y;
            mouseClickInInputField =  mousePosRelativeX > -1.32f && mousePosRelativeX < 8.72f
                && mousePosRelativeY > -2.52f && mousePosRelativeY < -2.03f;
            if (System.Math.Abs(mousePosRelativeX) > 1.5f && !mouseClickInInputField && !ScrollAreaPanelOpener.setActivation)
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
