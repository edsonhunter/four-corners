using Four_Corners.Domain.Interface;
using UnityEngine;

public class ElfController : MonoBehaviour
{
    private IElf Elf { get; set; }
    private bool UpdateStatus { get; set; }

    internal void Init(IElf elf)
    {
        Elf = elf;
        Elf.OnElfStatusUdate += OnElfStatusUpdate;
    }

    private void OnElfStatusUpdate()
    {
        UpdateStatus = true;
    }

    private void Update()
    {
        if (!Elf.Alive)
        {
            Destroy(gameObject);
            return;
        }

        if (!UpdateStatus)
        {
            return;
        }

        UpdateStatus = false;
        transform.position = new Vector3(Elf.CurrentTile.X, 0, Elf.CurrentTile.Y);
    }

    private void OnDestroy()
    {
        Elf.OnElfStatusUdate -= OnElfStatusUpdate;
    }
}