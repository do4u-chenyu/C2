namespace C2.Core.UndoRedo.Command
{
    // 测试Command,仅作测试UndoRedoManager功能使用
    class TestCommand : BaseCommand
    {
        public override bool _Redo()
        {
            System.Console.WriteLine("TestCommand: Do");
            return true;
        }

        public override bool _Undo()
        {
            System.Console.WriteLine("TestCommand: Rollback");
            return true;
        }
    }
}