using System;
using UnityEngine;
public class Jump
{
    private Rigidbody rb;
    private float jumpForce;
    public Jump(Rigidbody rb, float jumpForce)
    {
        this.rb = rb;
        this.jumpForce = jumpForce;
    }
    public void PerformJump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}