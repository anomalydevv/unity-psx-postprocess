# PS1 Retro Screen Effect for Unity

A lightweight, customizable post-processing camera script and shader for Unity that emulates classic PlayStation 1 (PSX) screen visuals, including pixelated low-resolution rendering, color quantization, and CRT scanlines.

---

## Features

- **Custom Resolution Downscaling:** Emulate classic internal resolutions like `320x240` or `256x224`.
- **Color Depth Restriction:** Simulates 15-bit color precision to recreate retro color banding.
- **CRT Scanlines:** Adjustable scanline overlay intensity.
- **Scene View Toggle:** Enable or disable the effect directly in the Editor's Scene view via the Inspector without stopping Play mode.
- **Lightweight & Fast:** Uses standard built-in rendering pipelines (`OnRenderImage` / `Graphics.Blit`).

---

## Installation

1. Download or clone this repository into your Unity project's `Assets` folder.
2. Attach the `RetroScreen.cs` script to your **Main Camera**.
3. Assign the `Hidden/PSX_PostProcess` shader to the **Shader Reference** slot in the Inspector.

---

## Inspector Settings

| Setting | Description | Default |
| :--- | :--- | :--- |
| **Show In Scene View** | Toggle rendering the effect inside Unity Scene View. | `true` |
| **Target Resolution** | Pixel grid resolution for the downscaled retro effect. | `(320, 240)` |
| **Color Depth** | Bit depth limit for color palette reduction. | `15` |
| **Scanline Intensity**| Visibility strength of horizontal CRT scanlines. | `0.12` |

---

## Compatibility

- **Unity Version:** Compatible with Unity 2019.4 LTS and newer.
- **Render Pipeline:** Built-in Render Pipeline (BiRP).

---

## License

This project is licensed under the MIT License - feel free to use it in commercial or non-commercial projects.
