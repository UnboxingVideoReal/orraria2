shader_type canvas_item;

// Hue is in normalized value [0.0, 1.0] where 0.0 means no change.
// For example, 0.1 shifts hue by 36° (0.1 * 360°).
uniform float hue_shift       : hint_range(-1.0, 1.0) = 0.0;
// Saturation multiplier. 1.0 means no change.
uniform float saturation_mult : hint_range(0.0, 2.0) = 1.0;
// Value (brightness in HSV) multiplier. 1.0 means no change.
uniform float value_mult      : hint_range(0.0, 2.0) = 1.0;
// Brightness addition in RGB space. 0.0 means no change.
uniform float brightness_add  : hint_range(-1.0, 1.0) = 0.0;
// Contrast multiplier in RGB space. 1.0 means no change.
uniform float contrast_mult   : hint_range(0.0, 2.0) = 1.0;

uniform float alpha       : hint_range(0.0, 1.0) = 1.0;

void fragment() {
    // Get the original texture color.
    vec4 tex_color = texture(TEXTURE, UV);
    vec3 col = tex_color.rgb;

	tex_color.a = tex_color.a * alpha;
    // Adjust brightness and contrast in RGB space.
    // The contrast formula shifts the color around mid-gray (0.5).

    // Output the final color while preserving the original alpha.
    COLOR.a = tex_color.a;
}
