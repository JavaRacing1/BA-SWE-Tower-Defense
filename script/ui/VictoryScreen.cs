using Godot;

public partial class VictoryScreen : Node
{
    private const double knightWaitTime = 1.8;
    private const double archerWaitTime = 3.6;
    private const double wizardWaitTime = 4.1;
    private const double skeletonWaitTime = 2.25;

    public override void _Ready()
    {
        AnimationPlayer knightPlayer = GetNode<AnimationPlayer>("Panel/VictoriousKnight/AnimationPlayer");
        AnimationPlayer archerPlayer = GetNode<AnimationPlayer>("Panel/VictoriousArcher/AnimationPlayer");
        AnimationPlayer wizardPlayer = GetNode<AnimationPlayer>("Panel/FallenWizard/AnimationPlayer");
        AnimationPlayer skeletonOnePlayer = GetNode<AnimationPlayer>("Panel/FallenSkeleton1/AnimationPlayer");
        AnimationPlayer skeletonTwoPlayer = GetNode<AnimationPlayer>("Panel/FallenSkeleton2/AnimationPlayer");
        Timer knightTimer = GetNode<Timer>("Panel/VictoriousKnight/TimerAttackKnight");
        Timer archerTimer = GetNode<Timer>("Panel/VictoriousArcher/TimerAttackArcher");
        Timer wizardTimer = GetNode<Timer>("Panel/FallenWizard/TimerDeathWizard");
        Timer skeletonTimer = GetNode<Timer>("Panel/TimerDeathSkeleton");

        knightPlayer.Play("AnimationsDefeatScreenKnight/KnightIdle");
        archerPlayer.Play("AnimationDefeatScreenArcher/ArcherIdle");
        wizardPlayer.Play("AnimationDefeatScreenWizard/WizardIdle");
        skeletonOnePlayer.Play("AnimationDefeatScreenSkeleton/Skeleton1Idle");
        skeletonTwoPlayer.Play("AnimationDefeatScreenSkeleton/Skeleton2Idle");

        knightTimer.WaitTime = knightWaitTime;
        knightTimer.Timeout += () => 
        {
            knightPlayer.Stop(false);
            knightPlayer.Play("AnimationsDefeatScreenKnight/KnightAttack");
            knightPlayer.Queue("AnimationsDefeatScreenKnight/KnightIdle");
        };
        knightTimer.Start();

        archerTimer.WaitTime = archerWaitTime;
        archerTimer.Timeout += () => 
        {
            archerPlayer.Stop(false);
            archerPlayer.Play("AnimationDefeatScreenArcher/ArcherAttack");
            archerPlayer.Queue("AnimationDefeatScreenArcher/ArcherIdle");
        };
        archerTimer.Start();

        wizardTimer.WaitTime = wizardWaitTime;
        wizardTimer.Timeout += () => 
        {
            wizardPlayer.Stop(false);
            wizardPlayer.Play("AnimationDefeatScreenWizard/WizardGoingDown");
        };
        wizardTimer.Start();

        skeletonTimer.WaitTime = skeletonWaitTime;
        skeletonTimer.Timeout += () =>
        {
            skeletonOnePlayer.Stop(false);
            skeletonTwoPlayer.Stop(false);
            skeletonOnePlayer.Play("AnimationDefeatScreenSkeleton/SkeletonGoingDown");
            skeletonTwoPlayer.Play("AnimationDefeatScreenSkeleton/SkeletonGoingDown");
        };
        skeletonTimer.Start();
    }

    private void OnMenuButtonPressed()
    {
        GetTree().ChangeSceneToFile("res://scene/ui/MainMenu.tscn");
        GameLevel gameLevel = GetNode<GameLevel>("/root/Level");
        if (gameLevel != null)
        {
            gameLevel.QueueFree();
        }
        GetTree().Paused = false;
    }
}