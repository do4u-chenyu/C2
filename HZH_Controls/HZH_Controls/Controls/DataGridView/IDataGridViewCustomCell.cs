namespace HZH_Controls.Controls
{
    public interface IDataGridViewCustomCell
    {
        /// <summary>
        /// 绑定行关联的数据
        /// </summary>
        /// <param name="obj">The object.</param>
        void SetBindSource(object obj);
    }
}
