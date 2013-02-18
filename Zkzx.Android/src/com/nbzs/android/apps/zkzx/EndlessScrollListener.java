package com.nbzs.android.apps.zkzx;

import android.widget.AbsListView;

/**
 * Created with IntelliJ IDEA.
 * User: Administrator
 * Date: 13-1-10
 * Time: 下午1:31
 * To change this template use File | Settings | File Templates.
 */
public class EndlessScrollListener implements AbsListView.OnScrollListener {

    private int visibleThreshold = 5;
    private int currentPage = 0;
    private int previousTotal = 0;
    private boolean loading = true;
    private MainDataArrayAdapter m_adapter;
    public EndlessScrollListener(MainDataArrayAdapter adapter) {
        m_adapter = adapter;
    }
    public EndlessScrollListener(int visibleThreshold) {
        this.visibleThreshold = visibleThreshold;
    }

    @Override
    public void onScroll(AbsListView view, int firstVisibleItem,
                         int visibleItemCount, int totalItemCount) {
        if (loading) {
            if (totalItemCount > previousTotal) {
                loading = false;
                previousTotal = totalItemCount;
                currentPage++;
            }
        }
        if (!loading && (totalItemCount - visibleItemCount) <= (firstVisibleItem + visibleThreshold)) {
            // I load the next page of gigs using a background task,
            // but you can call any function here.
            (new MainLoadDataTask(m_adapter)).execute(currentPage + 1);
            loading = true;
        }
    }

    @Override
    public void onScrollStateChanged(AbsListView view, int scrollState) {
    }
}
