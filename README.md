# USB-type-F

Water Drip Hazard - README
Overview
A reusable ceiling-mounted water drip trap for USB Type F. The droplet cycles through hanging, dropping, splashing, and resetting states without spawning or destroying objects, demonstrating efficient object reuse and state management.
Features

State-driven behavior: Clean state machine using boolean flags (isDropping, hasSplashed)
Physics integration: Toggles between Kinematic (hanging) and Dynamic (falling) Rigidbody states
Animation synchronization: Coroutine waits for splash animation length before resetting
Damage system: Implements IDamagable interface for player interaction
Zero garbage allocation: Single object teleports back to start position instead of Instantiate/Destroy pattern
Editor validation: OnValidate() prevents invalid timer configurations

How It Works
Lifecycle
1. IDLE (Kinematic at ceiling)
   ↓ (timer reaches dropDelay)
2. DROP (Dynamic, gravity enabled)
   ↓ (collision detected)
3. SPLASH (animation plays, collider disabled)
   ↓ (animation completes)
4. RESET (teleport to start position)
   ↓ (loop back to IDLE)
Key Components

Rigidbody2D: Switches between Kinematic (stationary) and Dynamic (physics-driven) modes
BoxCollider2D: Disabled during splash to prevent multi-collision bugs
Animator: Triggers "Drop" and "Splash" animation states
Coroutine: SplashAndReset() synchronizes splash animation timing with state reset

Technical Highlights
Performance Optimization
Instead of spawning/destroying objects every cycle:
csharp// ❌ Common beginner approach (causes GC spikes)
Instantiate(dropletPrefab);
Destroy(droplet);

// ✅ Our approach (zero allocations)
transform.position = startPosition; // Teleport back
Collision Safety
csharpwaterDropCollider.enabled = false; // Prevent re-entry during splash
yield return new WaitForSeconds(splashLength); // Wait for animation
waterDropCollider.enabled = true; // Re-enable after reset
Dynamic Animation Timing
csharpfloat splashLength = animator.GetCurrentAnimatorStateInfo(0).length;
yield return new WaitForSeconds(splashLength);
Automatically adapts to animation duration changes - no hardcoded delays.
Configuration
ParameterTypeDefaultDescriptiondropDelayfloat4.0sTime before droplet fallsdamageAmountfloat50Damage dealt to player on hitwaterDropRigidbodyRigidbody2D-Reference to droplet physics bodywaterDropColliderBoxCollider2D-Collision detection componentanimatorAnimator-Animation controller
Usage in Scene

Attach script to GameObject with water droplet sprite
Assign Rigidbody2D (set to Kinematic initially)
Assign BoxCollider2D
Assign Animator with "Drop" and "Splash" triggers
Position at desired ceiling location
Adjust dropDelay for difficulty tuning

Design Decisions
Why Single-Object Reuse?
This trap follows the Mario-style obstacle hazard pattern - predictable, rhythmic danger from a fixed location. Multiple simultaneous droplets would require object pooling, but the single-droplet design:

Simplifies player learning (one hazard = one rhythm to learn)
Reduces complexity (no pool management needed)
Maintains clean code (single responsibility per spawner)

Why Kinematic → Dynamic Toggle?

Kinematic when idle: Zero physics overhead, precise ceiling positioning
Dynamic when falling: Realistic gravity-driven drop with natural velocity

Why Disable Collider During Splash?
Prevents edge case where fast-moving player triggers OnCollisionEnter2D multiple times during the splash animation window, dealing unintended multi-hit damage.
Future Enhancements

 Warning indicator (sprite flashes 1 second before drop)
 Difficulty scaling (dropDelay decreases over time)
 Sound effects (drip + splash audio)
 Particle system for splash VFX
 Multiple droplet patterns (coordinated spawner arrays)

Code Quality Features

Serialized fields with [Header()] organization for clean Inspector
Summary documentation for class purpose
Validation guards prevent invalid configurations
Single Responsibility: Each method does one clear thing
Boolean prefixes (is, has) for readable state flags
